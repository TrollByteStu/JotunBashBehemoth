using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class rubberDucky : MonoBehaviour
{
    public AudioSource BombTicking;
    public AudioSource Squeaking;
    public int squeakDelay = 360;

    public bool stillTicking = true;

    public GameObject explosionPrefab;

    public bool isDud = true;
    public bool beingHeld = false;
    public bool beenPickedUp = false;
    public bool floating = false;

    private Rigidbody myRigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.transform.tag == "Water" && beenPickedUp )
        {
            GetComponent<WateverVolumeFloater>().enabled = true;
            myRigidBody.drag = 1f;
            myRigidBody.angularDrag = 1f;
            floating = true;
        }
    }

    public void eventSelect()
    {
        Debug.Log("Select");
        beingHeld = true;
        beenPickedUp = true;
        BombTicking.Play();
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
        Squeaking.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        if (Random.Range(1, 4) == 1) isDud = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( floating )
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }

        if ( !BombTicking.isPlaying && stillTicking && beingHeld)
        {
            if ( isDud )
            {
                stillTicking = false;
                Squeaking.Play();
            } else
            { // not a dud
                var explotion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explotion, 5);
                Destroy(gameObject);
            }
            
        }

        if ( !stillTicking && !Squeaking.isPlaying && beingHeld)
        {
            if ( Random.Range(0, squeakDelay) <= 1)
            {
                Squeaking.Play();
            }
        }
    }
}
