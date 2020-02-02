using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexController : MonoBehaviour
{
    public Transform waterSurface;
    public LayerMask waterSurfaceLayer;
    public float vortexRadialIntensity = 100f;
    public float vortexTangencialIntensity = 100f;
    private float maxRayDistace = 1000f;

    private InputController inputController;
    private Vortex vortex;
    private Ray clickRay;

    private float surfaceY { get { return waterSurface.position.y; } }

    private void Awake()
    {
        inputController = FindObjectOfType<InputController>();
        vortex = GetComponentInChildren<Vortex>();

        inputController.StartClick += StartVortex;
        inputController.MoveClick += MoveVortex;
        inputController.StopClick += StopVortex;
    }

    private void OnDestroy()
    {
        inputController.StartClick -= StartVortex;
        inputController.MoveClick -= MoveVortex;
        inputController.StopClick -= StopVortex;
    }

    private void StartVortex(Vector2 screenPosition)
    {
        clickRay = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(clickRay, out hit, maxRayDistace, waterSurfaceLayer, QueryTriggerInteraction.Collide))
        {
            vortex.StartVortex(hit.point, vortexRadialIntensity, vortexTangencialIntensity);
        }

        Events.Vortex.Start.SafeInvoke();
    }

    private void MoveVortex(Vector2 screenPosition)
    {
        clickRay = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(clickRay, out hit, maxRayDistace, waterSurfaceLayer, QueryTriggerInteraction.Collide))
        {
            vortex.MoveVortex(hit.point);
        }
        else
        {
            vortex.StopVortex();
        }
    }

    private void StopVortex(Vector2 screenPosition)
    {
        vortex.StopVortex();

        Events.Vortex.Stop.SafeInvoke();
    }

}
