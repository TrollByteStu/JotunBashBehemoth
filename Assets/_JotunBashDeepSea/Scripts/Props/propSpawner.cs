using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propSpawner : MonoBehaviour
{
    public GameObject spawnedPrefab;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1)
        {
            var spawned = Instantiate(spawnedPrefab,transform.position,transform.rotation, transform);
        }
    }
}
