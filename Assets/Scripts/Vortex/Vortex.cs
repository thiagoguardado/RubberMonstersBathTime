using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    private Animator animator;
    public LayerMask influencedLayers;
    public Color debugColor = Color.cyan;
    private float initiaScale;
    private bool isActive = false;
    private float radialIntensity;
    private float tangencialIntensity;
    private Collider[] hitColliders;
    public SpriteRenderer sprite;
    private float spriteSizeRatio;

    private float radius { get { return transform.localScale.x; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        initiaScale = transform.localScale.x;

        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = true;
        sprite.gameObject.SetActive(false);
        spriteSizeRatio = sprite.transform.localScale.x / radius;
    }

    private void Update()
    {
        InfluenceObjects();
    }

    public void StartVortex(Vector3 position, float radialIntensity, float tangencialIntensity, float radius = -1f)
    {
        this.transform.position = position;
        this.isActive = true;
        this.radialIntensity = radialIntensity;
        this.tangencialIntensity = tangencialIntensity;
        if (radius != -1f)
        {
            this.transform.localScale = Vector3.one * radius;
            sprite.transform.localScale = Vector3.one * spriteSizeRatio * radius;
        }

        sprite.gameObject.SetActive(true);

        animator.SetTrigger("startPulse");
    }

    public void MoveVortex(Vector3 position)
    {
        transform.position = position;
    }

    public void StopVortex()
    {
        this.isActive = false;

        sprite.gameObject.SetActive(false);
    }

    private void InfluenceObjects()
    {
        if (!isActive) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, influencedLayers);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            ForceInduced forceInduced = hitColliders[i].GetComponent<ForceInduced>();
            if (forceInduced != null)
            {
                float distance = (hitColliders[i].transform.position - transform.position).magnitude;

                Vector3 radialForceDirection = transform.position - forceInduced.transform.position;
                float radialIntensityAdjust = radialIntensity * 100 * distance / radius; // make force proportional to distance
                Vector3 radialForce = radialForceDirection.normalized * radialIntensityAdjust * Time.deltaTime;

                Vector3 tangentForceDirection = Vector3.Cross(radialForceDirection, Vector3.down);
                float tangencialIntensityAdjust = tangencialIntensity * 100 * (1 - distance / radius); // make force proportional to distance
                Vector3 tangencialForce = tangentForceDirection.normalized * tangencialIntensityAdjust * Time.deltaTime;

                forceInduced.AddForce(radialForce + tangencialForce);
            }
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = new Color(debugColor.r, debugColor.g, debugColor.b, 0.5f);
        Gizmos.DrawSphere(transform.position, radius);

    }
}
