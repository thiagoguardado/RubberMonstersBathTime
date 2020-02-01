using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToyBodyConfiguration: ScriptableObject
{
    public ToyBody BodyPrefab;
    public BodyPart PartPrefabRight;
    public BodyPart PartPrefabLeft;
    public BodyPartConfiguration[] BodyPartConfigurations;
}
