using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyFactoryEffects : MonoBehaviour
{
    private SFXDispatcher spawnDispatcher;
    private ToyBodyFactory toyBodyFactory;
    public GameObject vfxParticles;

    void Awake()
    {
        spawnDispatcher = GetComponent<SFXDispatcher>();
        toyBodyFactory = GetComponent<ToyBodyFactory>();

        toyBodyFactory.Spawn += OnSpawn;
    }

    void OnDestroy()
    {
        toyBodyFactory.Spawn += OnSpawn;
    }

    private void OnSpawn(Vector3 position)
    {
        // sfx
        spawnDispatcher.PlaySfxOnce();

        // vfx
        Instantiate(vfxParticles, position, vfxParticles.transform.rotation);
    }

}
