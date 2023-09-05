using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propSpawner : MonoBehaviour
{
    public GameObject spawnedPrefab;

    public bool useInventoryToSpawn = false;
    public bool onSpawnInScene = false;

    private GameController mainGC;

    public GameObject lastSpawn;



    void Start()
    {
        mainGC = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (onSpawnInScene)
        {
            if (lastSpawn == null)
            {
                lastSpawn = Instantiate(spawnedPrefab, transform.position, transform.rotation, transform);
            }
        }
        else
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
}
