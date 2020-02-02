using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EBodyPartSlot
{
    LEFT,
    RIGHT
}

public class ToyBodyFactory : MonoBehaviour
{
    public List<ToyBody> ToyBodies;
    public static ToyBodyFactory Instance;

    public ToyBodyConfiguration Configuration;

    public Transform BodyPartParent;

    [Header("Create instance debug")]
    public string id1;
    public EBodyPartSlot slot1;
    public string id2;
    public EBodyPartSlot slot2;
    public Rect SpawnArea;
    public List<BodyPart> BodyParts = new List<BodyPart>();

    public event Action<Vector3> Spawn;

    [ContextMenu("Create instance")]
    public void CreateInstance()
    {
        InstantiateBody(id1, slot1, id2, slot2, transform.position, transform.rotation);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            JoinTwo();
        }
    }

    public void JoinTwo()
    {
        for (int i = ToyBodies.Count - 2; i >= 0; i -= 2)
        {
            Debug.Log($"{i} joins {i + 1}");
            ToyBodies[i + 1].Join(ToyBodies[i]);
        }
    }

    private BodyPart InstantiatePart(string id, EBodyPartSlot slot)
    {
        var partConfigurations = Configuration.BodyPartConfigurations;
        foreach (var partConfiguration in partConfigurations)
        {
            if (partConfiguration.id != id)
            {
                continue;
            }

            BodyPart emptyPrefab;
            GameObject graphicsPrefab;

            switch (slot)
            {
                case EBodyPartSlot.LEFT:
                    emptyPrefab = Configuration.PartPrefabLeft;
                    graphicsPrefab = partConfiguration.LeftPrefab;
                    break;
                case EBodyPartSlot.RIGHT:
                    emptyPrefab = Configuration.PartPrefabRight;
                    graphicsPrefab = partConfiguration.RightPrefab;
                    break;
                default:
                    emptyPrefab = null;
                    graphicsPrefab = null;
                    break;
            }
            BodyPart emptyBodyPart = Instantiate<BodyPart>(emptyPrefab);
            GameObject bodyGraphics = Instantiate(graphicsPrefab, emptyBodyPart.ModelContainer.transform);
            bodyGraphics.transform.localPosition = Vector3.zero;
            bodyGraphics.transform.localScale = Vector3.one;
            bodyGraphics.transform.localRotation = Quaternion.identity;
            emptyBodyPart.TargetSlot = (int)slot;
            emptyBodyPart.OriginalSlot = (int)slot;
            emptyBodyPart.Id = id;
            BodyParts.Add(emptyBodyPart);
            return emptyBodyPart;
        }
        return null;
    }

    public ToyBody InstantiateBody(string bodyPart1, EBodyPartSlot slot1, string bodyPart2, EBodyPartSlot slot2, Vector3 position, Quaternion rotation)
    {
        var part1 = InstantiatePart(bodyPart1, slot1);
        var part2 = InstantiatePart(bodyPart2, slot2);
        var body = InstantiateBody(part1, part2, position, rotation);
        body.UpdateBodyPartPositions();
        
        if (part1 != null)
        {
            part1.UpdateImmediately();
            Spawn.SafeInvoke(part1.transform.position);
        }
        if (part2 != null)
        {
            part2.UpdateImmediately();
            Spawn.SafeInvoke(part2.transform.position);
        }

        return body;
    }

    public ToyBody InstantiateBody(BodyPart bodyPart1, BodyPart bodyPart2, Vector3 position, Quaternion rotation)
    {
        ToyBody toyBody = Instantiate<ToyBody>(Configuration.BodyPrefab, position, rotation);

        toyBody.BodyParts = new List<BodyPart>();
        if (bodyPart1 != null)
        {
            toyBody.BodyParts.Add(bodyPart1);
        }
        if (bodyPart2 != null)
        {
            toyBody.BodyParts.Add(bodyPart2);
        }

        ToyBodies.Add(toyBody);
        return toyBody;
    }

    internal List<string> GetActiveToyIds()
    {
        List<string> activeIds = new List<string>();
        foreach (BodyPart part in BodyParts)
        {
            if (!activeIds.Contains(part.Id))
            {
                activeIds.Add(part.Id);
            }
        }
        return activeIds;
    }
}
