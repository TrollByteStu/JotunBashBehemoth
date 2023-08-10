using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sodaCan : MonoBehaviour
{
    public bool canOpen = false;
    public int drinksLeft = 10;
    public bool beingHeld = false;

    public AudioSource SoundOpening;
    public AudioSource SoundDrinking;

    private Rigidbody myRigidBody;

    public void eventSelect()
    {
        Debug.Log("Select");
        beingHeld = true;
    }

    public void eventUnSelect()
    {
        Debug.Log("UnSelect");
        myRigidBody.isKinematic = false;
        transform.SetParent(null);
        Destroy(gameObject, 10f);
    }

    public void eventActivate()
    {
        if ( beingHeld && !canOpen)
        {
            canOpen = true;
            SoundOpening.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerFace" && canOpen && drinksLeft > 0)
        {
            drinksLeft--;
            SoundDrinking.Play();
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
