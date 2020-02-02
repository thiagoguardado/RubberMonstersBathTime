using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Mission
{
    int id;
    List<string> ids = new List<string>(2);
    int value;
    private bool isFullfilled;

    public int Id { get => id; }
    public string leftPartID { get => ids[0]; }
    public string rightPartID { get => ids[1]; }

    public Mission(int id, string id1, string id2, int value)
    {
        this.id = id;
        this.ids.Add(id1);
        this.ids.Add(id2);
        this.value = value;
        this.isFullfilled = false;
    }

    public bool FullfillMission(string id1, string id2)
    {
        if (!isFullfilled)
        {
            isFullfilled = ((ids[0] == id1 && ids[1] == id2) || (ids[0] == id2 && ids[1] == id1));
            return isFullfilled;
        }

        return false;
    }

    public bool CanBeFullfilled(List<string> availableIds)
    {
        List<string> availableIdsCopy = new List<string>(availableIds);
        if (availableIds.Remove(ids[0]))
        {
            if (availableIds.Remove(ids[1]))
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public struct MissionTableUnit
{
    public float duration;
    public string possibleIds;
    public int value;
    public float timeSpan; // seconds for new mission
}

public class MissionsController : MonoBehaviour
{
    public float initialDelay = 2f;
    public int maxActiveMissions = 3;
    public List<MissionTableUnit> missionsTable = new List<MissionTableUnit>();
    private List<Mission> activeMissions = new List<Mission>();
    private List<Mission> fulfilledMissions = new List<Mission>();

    private int currentLevel;
    private float lastTick;
    private float levelsTimer = 0f;
    private float missionTimer = 0f;
    private int createdMissions { get { return fulfilledMissions.Count + activeMissions.Count; } }

    public List<Mission> ActiveMissions { get => activeMissions; }

    private ToysController toyController;

    private void Awake()
    {
        toyController = FindObjectOfType<ToysController>();

        Events.Level.Start += StartMissions;
        Events.Timer.TickOverall += Tick;
        Events.Toys.Destroy += CheckMissionsFullfillment;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= StartMissions;
        Events.Timer.TickOverall -= Tick;
        Events.Toys.Destroy -= CheckMissionsFullfillment;
    }

    private void StartMissions()
    {
        currentLevel = 0;
        levelsTimer = -initialDelay;
        missionTimer = -initialDelay;
        lastTick = 0f;
    }

    private void Tick(float tick)
    {
        float spanTick = tick - lastTick;
        levelsTimer += spanTick;
        missionTimer += spanTick;

        if (currentLevel < (missionsTable.Count - 1) && levelsTimer >= missionsTable[currentLevel].duration)
        {
            // advance level 
            StartTableLevel(++currentLevel);
        }
        else if (missionTimer >= missionsTable[currentLevel].timeSpan)
        {
            // start new mission
            StartLevelMission();
        }

        lastTick = tick;

    }

    private void StartTableLevel(int tableLevel)
    {
        currentLevel = tableLevel;
        levelsTimer = 0f;

        StartLevelMission();
    }

    private void StartLevelMission()
    {
        missionTimer = 0f;

        string[] ids = missionsTable[currentLevel].possibleIds.Split(',');
        int value = missionsTable[currentLevel].value;

        CreateValidMission(ids, value);
    }

    public void CheckMissionsFullfillment(string id1, string id2)
    {
        for (int i = 0; i < activeMissions.Count; i++)
        {
            Mission mission = activeMissions[i];
            if (mission.FullfillMission(id1, id2))
            {
                activeMissions.Remove(mission);
                fulfilledMissions.Add(mission);

                Events.Missions.FulfillMission.SafeInvoke(mission);

                ToggleDrain();
                break;
            }
        }
    }

    private void ToggleDrain()
    {
        if (activeMissions.Count >= maxActiveMissions)
        {
            Events.Missions.MaxMissionsReached.SafeInvoke();
        }
        else
        {
            Events.Missions.MaxMissionsCleared.SafeInvoke();
        }
    }

    private void CreateValidMission(string[] ids, int value)
    {
        void CreateRandom()
        {
            string id1 = ids[UnityEngine.Random.Range(0, ids.Length)];
            string id2 = ids[UnityEngine.Random.Range(0, ids.Length)];

            CreateMission(id1, id2, value);
        }

        void CreatePossible(List<string> activeToys)
        {
            List<string> validIds = activeToys.Intersect(ids).ToList();
            if (validIds.Count > 1)
            {
                string id1 = validIds[UnityEngine.Random.Range(0, validIds.Count)];
                validIds.Remove(id1);
                string id2 = validIds[UnityEngine.Random.Range(0, validIds.Count)];

                CreateMission(id1, id2, value);
            }
            else
            {
                CreateRandom();
            }
        }

        if (activeMissions.Count >= maxActiveMissions) return;

        // verifies if active missions have possible solutions with active toys
        bool createdRandom = false;
        List<string> spawnedToys = toyController.ActiveToysIds;
        foreach (Mission mission in activeMissions)
        {
            if (mission.CanBeFullfilled(spawnedToys))
            {
                createdRandom = true;
                CreateRandom();
                return;
            }
        }

        // if no solution found with active toys, create possible mission
        if (!createdRandom && spawnedToys.Count > 0)
        {
            CreatePossible(spawnedToys);
            return;
        }

        // fallback
        CreateRandom();
    }

    private void CreateMission(string id1, string id2, int value)
    {
        Mission newMission = new Mission(createdMissions + 1, id1, id2, value);
        activeMissions.Add(newMission);

        Events.Missions.NewMission.SafeInvoke(newMission);

        ToggleDrain();
    }


}
