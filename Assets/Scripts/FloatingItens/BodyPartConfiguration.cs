using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyPartConfiguration
{
    public List<ToyBody> ToyBodies;
    public string id;
    public GameObject LeftPrefab;
    public GameObject RightPrefab;
}
