using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneric : MonoBehaviour
{
    public int _Damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        // do not allow it to hit the weapon that fired it..
        if (collision.transform.tag == "Weapon") return;
        else if (collision.transform.CompareTag("Boss") && collision.gameObject.GetComponent<PassiveMoby>())
        {
            collision.gameObject.GetComponent<PassiveMoby>().MobyHit(_Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponentInParent<Gannet>())
            collision.gameObject.GetComponentInParent<Gannet>().OnDeath();
        else if (collision.gameObject.GetComponent<circlingShark>())
            collision.gameObject.GetComponent<circlingShark>().Damage();
        else if (collision.transform.tag == "Water" )
        {
            GameObject decal = Instantiate(GameController.Instance.gcResources.Splashes[0], transform.position, Quaternion.identity);
            Destroy(decal, 4f);
        } 
        else { 
            // old buttelhole thingy
        }
        //decal.GetComponent<AudioSource>().Play();
       // decal.transform.SetParent(collision.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water")
        {
            GameObject decal = Instantiate(GameController.Instance.gcResources.Splashes[0], transform.position, Quaternion.identity);
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
