using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysController : MonoBehaviour
{
    public List<string> ActiveToysIds { get { return ToyBodyFactory.Instance.GetActiveToyIds(); } }

    private Coroutine spawnCoroutine;
    public void Start()
    {
        Events.Level.Start += OnLevelStart;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= OnLevelStart;
    }
    private void OnLevelStart()
    {
        if(spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnCR());
    }

    public IEnumerator SpawnCR()
    {
        Debug.Log("Spawn routine started");
        yield return new WaitForSeconds(1.0f);
        while(true)
        {
            Debug.Log("Starting spawn wave");
            for(int i = 0; i < 2; i++)
            {
                Debug.Log("Spawning");
                Spawn();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void Spawn()
    {
        int randomSlot = UnityEngine.Random.Range(0,2);
        var configurations = ToyBodyFactory.Instance.Configuration.BodyPartConfigurations;
        int randomIdIndex = UnityEngine.Random.Range(0, configurations.Length);
        string randomId = configurations[randomIdIndex].id;
        ToyBodyFactory.Instance.InstantiateBody(randomId, (EBodyPartSlot) randomSlot, "", EBodyPartSlot.LEFT, Vector3.zero, Quaternion.identity);
    }
}
