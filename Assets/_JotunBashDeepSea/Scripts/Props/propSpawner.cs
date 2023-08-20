using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propSpawner : MonoBehaviour
{
    public GameObject spawnedPrefab;

    public bool useInventoryToSpawn = false;

    private GameController mainGC;

    void Start()
    {
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( useInventoryToSpawn )
        {
            if ( mainGC.gcInventory.ItemHowMany(spawnedPrefab) >= 1 && transform.childCount < 1)
            {
                mainGC.gcInventory.inventory[spawnedPrefab]--;
                var spawned = Instantiate(spawnedPrefab, transform.position, transform.rotation, transform);
            }
        } else {
            if (transform.childCount < 1)
            {
                var spawned = Instantiate(spawnedPrefab, transform.position, transform.rotation, transform);
            }
        }
    }
}
