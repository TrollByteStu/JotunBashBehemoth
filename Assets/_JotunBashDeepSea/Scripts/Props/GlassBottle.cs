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
        if (collision.transform.tag == "Ammo")
        {
            wholeBottle.SetActive(false);
            fracturedBottle.SetActive(true);
            Destroy(gameObject, 5f);
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
