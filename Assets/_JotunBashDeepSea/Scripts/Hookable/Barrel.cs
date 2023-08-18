using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject AddInventoryPrefab;
    public int AddInventoryAmount = 10;

    private GameController mainGC;
    private Rigidbody myRigidBody;

    private WindZone mainWind;

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.transform.tag == "Raft")
        { // this has been picked up, add to inventory
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
        myRigidBody = GetComponent<Rigidbody>();
        mainWind = mainGC.gcWeather.mainWind;
    }

    // Update is called once per frame
    void Update()
    {
        // bobbing around in the wind
        myRigidBody.AddForce(mainWind.transform.forward * (mainWind.windMain * 0.1f) + Random.insideUnitSphere * mainWind.windTurbulence, ForceMode.Force);

        // fallen off the map, delete them
        if (transform.position.y < -100) Destroy(gameObject);
    }
}
