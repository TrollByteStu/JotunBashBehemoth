using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneric : MonoBehaviour
{
    public GameController mainGC;

    private void OnCollisionEnter(Collision collision)
    {
        // do not allow it to hit the weapon that fired it..
        if (collision.transform.tag == "Weapon") return;
        ContactPoint contact = collision.contacts[0];
        GameObject decal = Instantiate(mainGC.gcResources.BulletHoles[0], contact.point, Quaternion.LookRotation(collision.contacts[0].normal));
        decal.GetComponent<AudioSource>().Play();
        decal.transform.SetParent(collision.transform);
        Destroy(decal, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
