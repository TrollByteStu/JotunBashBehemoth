using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actorSpawner : MonoBehaviour
{
    public GameObject spawnedPrefab;

    public float afterSpawnDisableTimer = 120f;
    public float randomSpawnInhibitor = 3600f;

    private float lastSpawn = 0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1 && lastSpawn <= 0f && Random.Range(1, randomSpawnInhibitor) == 1)
        {
            var spawned = Instantiate(spawnedPrefab, transform.position, transform.rotation, transform);
            lastSpawn = afterSpawnDisableTimer;
        }
        lastSpawn -= Time.deltaTime;
    }
}
