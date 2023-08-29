using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class furniture : MonoBehaviour
{

    private bool touched = false;
    private bool floating = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water" && touched && !floating)
        {
            GameObject spawn;
            Quaternion spawnDirection;
            spawn = GameController.Instance.gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
            gameObject.AddComponent<OurWateverVolumeFloater>();
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Barrel>().enabled = true;
            floating = true;
        }
    }

    public void eventSelect()
    {
        Debug.Log("Rod Select");
        touched = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
