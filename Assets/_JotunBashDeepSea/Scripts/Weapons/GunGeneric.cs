using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
//using UnityEngine.UI;

public class GunGeneric : MonoBehaviour
{
    public UnityEngine.XR.InputDevice leftController;
    public UnityEngine.XR.InputDevice rightController;
    public float updateControllerTimer = 0f;
    public bool leftTrigger = false;
    public bool rightTrigger = false;

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

    private void updateController()
    {
        if (Application.isEditor) return;
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        leftController = leftHandDevices[0];
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        rightController = rightHandDevices[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        mainGC = GameObject.Find("_GameController").GetComponent<GameController>();
    }

    void fireGun()
    {
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
        if (!beingHeld) return;
        leftController.IsPressed(InputHelpers.Button.Trigger, out leftTrigger);
        rightController.IsPressed(InputHelpers.Button.Trigger, out rightTrigger);
        if (rightTrigger && fireDelay < 0f ) fireGun();
        if (transform.forward.y < -.75f && Bullets < clipsize ) reloadGun();
        fireDelay -= Time.deltaTime;
        updateControllerTimer -= Time.deltaTime;
        if (!(updateControllerTimer < 0f)) return;
        updateController();
        updateControllerTimer = 2f;
    }
}
