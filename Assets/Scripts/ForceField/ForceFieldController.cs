using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    public float baseForce;
    public float outerForce;
    private float _baseForce;
    private float _outerForce;

    public List<ForceField> baseFields;
    public List<ForceField> outerFields;

    private void Awake()
    {
        Events.Level.Start += StartFields;
        Events.Level.Finish += StopFields;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= StartFields;
        Events.Level.Finish -= StopFields;
    }

    private void Start()
    {
        UpdateForceFields();
    }

    private void Update()
    {
        UpdateForceFields();
    }

    private void UpdateForceFields()
    {
        if (baseForce != _baseForce)
        {
            _baseForce = baseForce;
            UpdateFieldsList(baseFields, _baseForce);
        }

        if (outerForce != _outerForce)
        {
            _outerForce = outerForce;
            UpdateFieldsList(outerFields, _outerForce);
        }
    }

    private void StopFields()
    {
        UpdateFieldsList(baseFields, 0f);
        UpdateFieldsList(outerFields, 0f);
    }

    private void StartFields()
    {
        UpdateFieldsList(baseFields, _baseForce);
        UpdateFieldsList(outerFields, _outerForce);
    }

    private void UpdateFieldsList(List<ForceField> fields, float force)
    {
        foreach (ForceField field in fields)
        {
            field.intensity = force;
        }
    }
}
