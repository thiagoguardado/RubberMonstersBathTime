using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EBodyPartSlot
{
    LEFT,
    RIGHT
}

[System.Serializable]
public class BodyPartConfiguration
{
    public List<ToyBody> ToyBodies;
    public string id;
    public string ToyId;
    public GameObject Prefab;
    public EBodyPartSlot TargetSlot;
    public Sprite uiImage;
}
public class ToyBodyFactory : MonoBehaviour
{
    public List<ToyBody> ToyBodies;
    public static ToyBodyFactory Instance;

    public ToyBodyConfiguration Configuration;

    public Transform BodyPartParent;

    [Header("Create instance debug")]
    public string id1;
    public string id2;
    public Rect SpawnArea;

    [ContextMenu("Create instance")]
    public void CreateInstance()
    {
        InstantiateBody(id1, id2, transform.position, transform.rotation);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            JoinTwo();
        }
    }

    public void JoinTwo()
    {
        for(int i = ToyBodies.Count-2; i >= 0; i-=2)
        {
            Debug.Log($"{i} joins {i+1}");
            ToyBodies[i+1].Join(ToyBodies[i]);
        }
    }
    
    private BodyPart InstantiatePart(string id)
    {
        var partConfigurations = Configuration.BodyPartConfigurations;
        foreach(var partConfiguration in partConfigurations)
        {
            if(partConfiguration.id != id)
            {
                continue;
            }

            var prefab = (partConfiguration.TargetSlot == EBodyPartSlot.LEFT) ? Configuration.PartPrefabLeft : Configuration.PartPrefabRight;
            BodyPart emptyBodyPart = Instantiate<BodyPart>(prefab);
            GameObject bodyGraphics = Instantiate(partConfiguration.Prefab, emptyBodyPart.ModelContainer.transform);
            bodyGraphics.transform.localPosition = Vector3.zero;
            bodyGraphics.transform.localScale = Vector3.one;
            bodyGraphics.transform.localRotation = Quaternion.identity;
            emptyBodyPart.TargetSlot = (int) partConfiguration.TargetSlot;
            emptyBodyPart.OriginalSlot = (int) partConfiguration.TargetSlot;
            return emptyBodyPart;
        }
        return null;
    }

    public ToyBody InstantiateBody(string bodyPart1, string bodyPart2, Vector3 position, Quaternion rotation)
    {
        var part1 = InstantiatePart(bodyPart1);
        var part2 = InstantiatePart(bodyPart2);
        var body = InstantiateBody(part1, part2, position, rotation);
        body.UpdateBodyPartPositions(); 
        part1.UpdateImmediately();
        part2.UpdateImmediately();
        return body;
    }

    public ToyBody InstantiateBody(BodyPart bodyPart1, BodyPart bodyPart2, Vector3 position, Quaternion rotation)
    {
        ToyBody toyBody = Instantiate<ToyBody>(Configuration.BodyPrefab, position, rotation);

        toyBody.BodyParts = new List<BodyPart>();
        if(bodyPart1 != null)
        {
            toyBody.BodyParts.Add(bodyPart1);
            //bodyPart1.transform.parent = toyBody.transform;
        }
        if(bodyPart2 != null)
        {
            toyBody.BodyParts.Add(bodyPart2);
           // bodyPart2.transform.parent = toyBody.transform;
        }
    
        ToyBodies.Add(toyBody);
        return toyBody;
    }
}
