using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBodyFactory : MonoBehaviour
{
    public List<ToyBody> ToyBodies;
    public static ToyBodyFactory Instance;
    public ToyBody Prefab;
    public BodyPart[] BodyPartPrefabs;

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

    public ToyBody InstantiateBody(BodyPart bodyPart, Vector3 position, Quaternion rotation)
    {
        ToyBody toyBody = Instantiate<ToyBody>(Prefab, position, rotation);
        toyBody.BodyParts = new List<BodyPart>();
        toyBody.BodyParts.Add(bodyPart);
        ToyBodies.Add(toyBody);
        return toyBody;
    }
}
