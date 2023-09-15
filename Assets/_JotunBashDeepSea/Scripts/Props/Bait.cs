using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class Bait : MonoBehaviour
{
    public List<string> baitWorkOnPrefabs;
    public bool ActivateBaitOnHittingWater = false;
    public bool ActivateBaitOnThrowing = false;
    public bool DoesThisBaitFloat = false;

    public float TimeToLiveAfterThrow = 30f;

    public bool ActiveBaitInWorld = false;
    public bool beingHeld = false;
    public bool beenPickedUp = false;
    public bool floating = false;

    private GameController mainGC;
    private Rigidbody myRigidBody;

    public bool doesThisBaitWorkOnMe(string species)
    {
        if (baitWorkOnPrefabs.Contains(species))
            return true;
        else 
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water" && beenPickedUp && !floating)
        {
            if (DoesThisBaitFloat)
            {
                GetComponent<WateverVolumeFloater>().enabled = true;
                myRigidBody.drag = 1f;
                myRigidBody.angularDrag = 1f;
                floating = true;
            }
            if (ActivateBaitOnHittingWater)
                ActivateBait();
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        mainGC = GameController.Instance;
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void eventSelect()
    {
        beingHeld = true;
        beenPickedUp = true;
    }

    public void eventUnSelect()
    {
        myRigidBody.isKinematic = false;
        beingHeld = false;
        transform.SetParent(null);
        if (ActivateBaitOnThrowing)
        {
            mainGC.activeBait.Add(this);
            ActiveBaitInWorld = true;
        }
        Destroy(gameObject, TimeToLiveAfterThrow);
    }

    public void ActivateBait()
    {
        if (ActiveBaitInWorld) return;
        GameController.Instance.activeBait.Add(this);
        ActiveBaitInWorld = true;
    }

    private void OnDisable()
    {
        if (ActiveBaitInWorld ) mainGC.activeBait.Remove(this);
    }

}
