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
            foreach (ForceField field in baseFields)
            {
                field.intensity = baseForce;
            }
        }

        if (outerForce != _outerForce)
        {
            _outerForce = outerForce;
            foreach (ForceField field in outerFields)
            {
                field.intensity = outerForce;
            }
        }
    }
}
