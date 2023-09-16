using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class Barrel : MonoBehaviour
{
    public GameObject AddInventoryPrefab;
    public int AddInventoryAmount = 10;

    public int AddBarrels = 0;
    public int AddPlanks = 0;
    public int AddLogs = 0;

    public bool floating = false;

    private Rigidbody myRigidBody;

    private WindZone mainWind;

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.transform.tag == "Raft")
        { // this has been picked up, add to inventory
            if ( AddInventoryPrefab != null ) GameController.Instance.gcInventory.itemAdd(AddInventoryPrefab, AddInventoryAmount);
            if (AddBarrels > 0) GameController.Instance.gcInventory.Barrels += AddBarrels;
            if (AddPlanks > 0) GameController.Instance.gcInventory.Planks += AddPlanks;
            if (AddLogs > 0) GameController.Instance.gcInventory.Logs += AddLogs;
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mainWind = GameController.Instance.gcWeather.mainWind;
    }

    // Update is called once per frame
    void Update()
    {
        // bobbing around in the wind
        myRigidBody.AddForce(mainWind.transform.forward * (mainWind.windMain * 1f) + Random.insideUnitSphere * mainWind.windTurbulence, ForceMode.Force);

        // fallen off the map, delete them
        if (transform.position.y < -100) Destroy(gameObject);
    }
}
