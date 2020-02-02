using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ToysTableUnit
{
    public float duration;
    public string possibleIds;
    public int maxToys;
    public float timeSpan; // seconds for new toy

}
public class ToysController : MonoBehaviour
{
    public Transform limits_minX;
    public Transform limits_maxX;
    public Transform limits_minZ;
    public Transform limits_maxZ;

    public float initialDelay = 2f;
    public float retryDelay = 0.5f;
    public List<ToysTableUnit> toysTable = new List<ToysTableUnit>();

    private int toysInbath = 0;
    private int currentLevel;
    private float lastTick;
    private float levelsTimer = 0f;
    private float toySpawnTimer = 0f;

    public event Action<string, string> ToyRemoved;

    public List<string> ActiveToysIds { get { return ToyBodyFactory.Instance.GetActiveToyIds(); } }

    public void Awake()
    {
        Events.Level.Start += StartToys;
        Events.Timer.Tick += Tick;
        Events.Toys.Destroy += OnToyDestroyed;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= StartToys;
        Events.Timer.Tick -= Tick;
        Events.Toys.Destroy -= OnToyDestroyed;
    }

    private void StartToys()
    {
        currentLevel = 0;
        levelsTimer = -initialDelay;
        toySpawnTimer = -initialDelay;
        lastTick = 0f;
    }

    private void Tick(float tick)
    {
        levelsTimer += tick;
        toySpawnTimer += tick;

        if (currentLevel < (toysTable.Count - 1) && levelsTimer >= toysTable[currentLevel].duration)
        {
            // advance level 
            StartTableLevel(++currentLevel);
        }
        else if (toySpawnTimer >= toysTable[currentLevel].timeSpan)
        {
            // start new mission
            LevelSpawn();
        }

        lastTick = tick;

    }

    private void StartTableLevel(int tableLevel)
    {
        currentLevel = tableLevel;
        levelsTimer = 0f;

        LevelSpawn();
    }

    private void LevelSpawn()
    {
        if (TrySpawnPair())
        {
            toySpawnTimer = 0f;
        }
        else
        {
            // schedulle retry
            toySpawnTimer = toysTable[currentLevel].timeSpan - retryDelay;
        }
    }

    private bool TrySpawnPair()
    {
        if (toysTable[currentLevel].maxToys > toysInbath)
        {
            toySpawnTimer = 0f;
            string[] ids = toysTable[currentLevel].possibleIds.Split(',');
            string id = ids[UnityEngine.Random.Range(0, ids.Length)];
            SpawnPair(id);
            return true;
        }

        return false;
    }

    private void SpawnPair(string id)
    {
        for (int i = 0; i < 2; i++)
        {
            int slot = i;
            Vector3 position = Vector3.zero;
            position.x += UnityEngine.Random.Range(limits_minX.position.x, limits_maxX.position.x);
            position.z += UnityEngine.Random.Range(limits_minZ.position.z, limits_maxZ.position.z);
            float angle = UnityEngine.Random.Range(0, 360);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);

            ToyBodyFactory.Instance.InstantiateBody(id, (EBodyPartSlot)slot, "", EBodyPartSlot.LEFT, position, rotation);
        }

        toysInbath++;
    }

    private void OnToyDestroyed(string id1, string id2)
    {
        toysInbath--;
    }
}
