﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Color color = Color.red;
    public Vector3 direction;
    public float intensity;
    public LayerMask layersInfluenced;
    
    private Vector3 directionNormalized { get { return direction.normalized; } }
    Collider collider;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        InduceObjectsInside();
    }

    void InduceObjectsInside()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, collider.bounds.extents, Quaternion.identity, layersInfluenced);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            ForceInduced forceInduced = hitColliders[i].GetComponent<ForceInduced>();
            if(forceInduced!=null) forceInduced.AddForce(directionNormalized * intensity * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r,color.g,color.b,0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
