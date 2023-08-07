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
    private XRGrabInteractable myXrGrab;

    private XRDirectInteractor interactor = null;
    public bool IsGrabbing = false;
/*
    private void OnEnable()
    {

        myXrGrab.onSelectEntered.AddListener(TakeInput);
        myXrGrab.onSelectExited.AddListener(StopInput);

    }

    private void OnDisable()
    {

        myXrGrab.onSelectEntered.RemoveListener(TakeInput);
        myXrGrab.onSelectExited.RemoveListener(StopInput);

    }
*/
    public void eventFocus()
    {
        Debug.Log("Focus");
    }

    public void eventSelect()
    {
        Debug.Log("Select");
    }

    public void eventActivate()
    {
        Debug.Log("Active");
    }
    private void TakeInput(XRBaseInteractor interactable)
    {

        IsGrabbing = true;
        Debug.Log("is grabbing");

    }

    private void StopInput(XRBaseInteractor interactable)
    {

        IsGrabbing = false;
        Debug.Log("is releasing");

    }

    private void updateController()
    {
        if (myXrGrab.isActiveAndEnabled) ReloadSound.Play();
        if (myXrGrab.isSelected) EmptySound.Play();
        if (myXrGrab.isFocused) FireSound.Play();
  
        /*
        if (Application.isEditor) return;
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        leftController = leftHandDevices[0];
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        rightController = rightHandDevices[0];
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
        myXrGrab = GetComponent<XRGrabInteractable>();
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
        
        /*
        if (!beingHeld) return;
        leftController.IsPressed(InputHelpers.Button.Trigger, out leftTrigger);
        rightController.IsPressed(InputHelpers.Button.Trigger, out rightTrigger);
        if (rightTrigger && fireDelay < 0f ) fireGun();
        if (transform.forward.y < -.75f && Bullets < clipsize ) reloadGun();
        fireDelay -= Time.deltaTime;
        updateControllerTimer -= Time.deltaTime;
        if (!(updateControllerTimer < 0f)) return;
        */
        updateController();
        updateControllerTimer = 1f;
    }
}
