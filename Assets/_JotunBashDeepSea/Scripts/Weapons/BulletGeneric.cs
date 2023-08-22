using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneric : MonoBehaviour
{
    public GameController mainGC;
    public int _Damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        // do not allow it to hit the weapon that fired it..
        if (collision.transform.tag == "Weapon") return;
        if (collision.transform.CompareTag("Boss") && collision.gameObject.GetComponent<PassiveMoby>())
            collision.gameObject.GetComponent<PassiveMoby>().MobyHit(_Damage);
        if (collision.gameObject.GetComponent<Gannet>())
            collision.gameObject.GetComponent<Gannet>().OnDeath();
        GameObject spawn;
        Quaternion spawnDirection;
        if (collision.transform.tag == "Water" )
        {
            spawn = mainGC.gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
        } else { 
            spawn = mainGC.gcResources.BulletHoles[0];
            spawnDirection = Quaternion.LookRotation(collision.contacts[0].normal);
        }
        ContactPoint contact = collision.contacts[0];
        GameObject decal = Instantiate(spawn, contact.point, spawnDirection);
        //decal.GetComponent<AudioSource>().Play();
        decal.transform.SetParent(collision.transform);
        Destroy(decal, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water")
        {
            GameObject spawn;
            Quaternion spawnDirection;
            spawn = mainGC.gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
        }
    }

    //private void onCollisionEnter(Collision collision)
    //{
    //    // do not allow it to hit the weapon that fired it..
    //    if (collision.transform.tag == "Weapon") return;
    //    // Will add logic for diffeent decals and sound on hitting different materials, we hope
    //    GameObject spawn;
    //    Quaternion spawnDirection;
    //    spawn = mainGC.gcResources.BulletHoles[0];
    //    spawnDirection = Quaternion.LookRotation(collision.contacts[0].normal);
    //    ContactPoint contact = collision.contacts[0];
    //    GameObject decal = Instantiate(spawn, contact.point, spawnDirection);
    //    decal.GetComponent<AudioSource>().Play();
    //    decal.transform.SetParent(collision.transform);
    //    Destroy(decal, 2f);
    //}

}
