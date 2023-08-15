using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public List<GameObject> baitWorkOnPrefabs;

    private GameController mainGC;
    private Rigidbody myRigidBody;

    public bool doesThisBaitWorkOnMe(GameObject myPrefab)
    {
        if (baitWorkOnPrefabs.Contains(myPrefab))
            return true;
        else 
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
        mainGC.activeBait.Add(this);
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        mainGC.activeBait.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}