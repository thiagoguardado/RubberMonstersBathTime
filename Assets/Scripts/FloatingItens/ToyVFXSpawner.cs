using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyVFXSpawner : MonoBehaviour
{
    private ToyJoinReaction toyJoinReaction;
    private ToyBody toyBody;
    public GameObject collisionParticles;
    public GameObject completedJoinParticles;

    private void Awake()
    {
        toyJoinReaction = GetComponentInParent<ToyJoinReaction>();
        toyBody = GetComponentInParent<ToyBody>();

        toyBody.Splitted += EmitCollision;
        toyBody.Joined += EmitCollision;
        toyJoinReaction.CompletedJoinedReaction += EmitCompletedJoin;
        
    }

    private void OnDestroy()
    {
        toyBody.Splitted -= EmitCollision;
        toyBody.Joined -= EmitCollision;
        toyJoinReaction.CompletedJoinedReaction -= EmitCompletedJoin;
    }

    private void EmitCollision()
    {
        Instantiate(collisionParticles, transform.position, Quaternion.identity);
    }

    private void EmitCompletedJoin()
    {
        Instantiate(completedJoinParticles, transform.position, Quaternion.identity);
    }
}
