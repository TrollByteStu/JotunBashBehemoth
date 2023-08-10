using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
//using UnityEngine.UI;

public class GunGeneric : MonoBehaviour
{

    public GameObject prefabBullet;
    public Transform muzzleLocation;
    public AudioSource FireSound;
    public AudioSource EmptySound;
    public AudioSource ReloadSound;

    public bool beingHeld = false;

    public float fireDelayMax = 0.2f;
    public float fireDelay = 0f;
    public float bulletForce = 2000f;

    public int Bullets = 10;
    public int clipsize = 10;
    public float reloadDelay = 1f;

    private GameController mainGC;
    private XRGrabInteractable myGrab;
    private Rigidbody myRigidBody;

    public void eventSelect()
    {
        Debug.Log("Select");
        beingHeld = true;
    }
    public void eventUnSelect()
    {
        Debug.Log("UnSelect");
        beingHeld = false;
        myRigidBody.isKinematic = false;
        transform.SetParent(null);
    }

    public void eventActivate()
    {
        Debug.Log("Active");
        fireGun();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
        myGrab = GetComponent<XRGrabInteractable>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void fireGun()
    {
        if (fireDelay > 0f) return;
        if ( Bullets <= 0 )
        {
            if (EmptySound) EmptySound.Play();
            fireDelay = reloadDelay;
            return;
        }
        GameObject newBullet = Instantiate(prefabBullet);
        newBullet.transform.position = muzzleLocation.position;
        newBullet.transform.rotation = muzzleLocation.rotation;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletForce);
        newBullet.GetComponent<BulletGeneric>().mainGC = mainGC;
        Destroy(newBullet, 5f);
        fireDelay = fireDelayMax;
        Bullets--;
        if (FireSound) FireSound.Play();
    }

    void reloadGun()
    {
        if (ReloadSound) ReloadSound.Play();
        Bullets = clipsize;
    }
    // Update is called once per frame
    void Update()
    {
        fireDelay -= Time.deltaTime;
        if (transform.forward.y < -.75f && Bullets < clipsize) reloadGun();
    }
}
