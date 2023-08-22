using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBottle : MonoBehaviour
{
    public GameObject wholeBottle;
    public GameObject fracturedBottle;

    private Rigidbody myRigidBody;


    public void eventSelect()
    {

    }

    public void eventUnselect()
    {
        myRigidBody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ammo" || myRigidBody.velocity.magnitude > 4f)
        {
            wholeBottle.SetActive(false);
            fracturedBottle.SetActive(true);
            Destroy(gameObject, 5f);
        }
        Debug.Log("Glass hit something at speed : "+myRigidBody.velocity.magnitude.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water")
        {
            GameObject spawn;
            Quaternion spawnDirection;
            spawn = GameObject.Find("GameController").GetComponent<GameController>().gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
