using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    //spawns props, actors or a mix
    //spawns inside the box colliders area

    public GameObject[] spawnedPrefabs;
    public int spawnAmount = 1;
    public int spawnMinimum = 0;

    public bool spawnStartThenStop = false;

    public float afterSpawnDisableTimer = 30f;
    public float randomSpawnInhibitor = 360f;

    private float lastSpawn = 0f;

    private BoxCollider myCollider;
    private Vector3 spawnMin;
    private Vector3 spawnMax;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        spawnMin = myCollider.bounds.min;
        spawnMax = myCollider.bounds.max;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < spawnAmount && lastSpawn <= 0f && Random.Range(0, randomSpawnInhibitor) <= 1 || transform.childCount < spawnMinimum )
        {
            var spawned = Instantiate(spawnedPrefabs[Random.Range(0,spawnedPrefabs.Length)],
                new Vector3(Random.Range(spawnMin.x,spawnMax.x), Random.Range(spawnMin.y, spawnMax.y), Random.Range(spawnMin.z, spawnMax.z)), 
                Quaternion.identity, 
                transform);
            lastSpawn = afterSpawnDisableTimer;
            
        }
        if (spawnStartThenStop && transform.childCount == spawnMinimum)
        {
            while ( transform.childCount > 0)
            {
                transform.GetChild(0).SetParent(null);
            }
            gameObject.SetActive(false);
        }
        lastSpawn -= Time.deltaTime;
    }
}
