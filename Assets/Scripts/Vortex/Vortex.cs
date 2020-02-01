using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    private float radius = 10f;
    public LayerMask influencedLayers;
    public Color debugColor = Color.cyan;

    private bool isActive = false;
    private Vector3 pivot;
    private float radialIntensity;
    private float tangencialIntensity;
    private Collider[] hitColliders;

    private void Update()
    {
        InfluenceObjects();
    }

    public void StartVortex(Vector3 position, float radialIntensity, float tangencialIntensity, float radius)
    {
        this.isActive = true;
        this.pivot = position;
        this.radialIntensity = radialIntensity;
        this.tangencialIntensity = tangencialIntensity;
        this.radius = radius;
    }

    public void MoveVortex(Vector3 position)
    {
        this.pivot = position;
    }

    public void StopVortex()
    {
        this.isActive = false;
    }

    private void InfluenceObjects()
    {
        if (!isActive) return;

        Collider[] hitColliders = Physics.OverlapSphere(pivot, radius, influencedLayers);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            ForceInduced forceInduced = hitColliders[i].GetComponent<ForceInduced>();
            if (forceInduced != null)
            {
                float distance = (hitColliders[i].transform.position - pivot).magnitude;

                Vector3 radialForceDirection = pivot - forceInduced.transform.position;
                float radialIntensityAdjust = radialIntensity * 100 * distance / radius; // make force proportional to distance
                Vector3 radialForce = radialForceDirection.normalized * radialIntensityAdjust * Time.deltaTime;

                Vector3 tangentForceDirection = Vector3.Cross(radialForceDirection, Vector3.down);
                float tangencialIntensityAdjust = tangencialIntensity * 100 * (1 - distance / radius); // make force proportional to distance
                Vector3 tangencialForce = tangentForceDirection.normalized * tangencialIntensityAdjust * Time.deltaTime;

                forceInduced.AddForce(radialForce + tangencialForce);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (isActive)
        {
            Gizmos.color = new Color(debugColor.r, debugColor.g, debugColor.b, 0.5f);
            Gizmos.DrawSphere(pivot, radius);
        }
    }
}
