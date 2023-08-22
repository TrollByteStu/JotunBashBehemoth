using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{
    private Rigidbody myRB;
    public int _Damage = 1;
    public bool _Touched = false;
    public bool beingHeld = false;
    public bool _StuckInEnemy = false;
    public bool _Stuck = false;
    private float _DestroyTime;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= -100 || _DestroyTime + 20 <= Time.time && _Stuck)
            Destroy(gameObject);
        if (_Touched && !beingHeld && !_Stuck)
            transform.LookAt(transform.position + myRB.velocity * 10);
    }

    public void eventSelect()
    {
        beingHeld = true;
        _Touched = true;
    }

    public void eventUnSelect()
    {
        beingHeld = false;
        myRB.isKinematic = false;
        transform.SetParent(null);

    }

    public void eventActivate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_Touched && !beingHeld)
        {
            if (collision.transform.CompareTag("Boss") && collision.gameObject.GetComponent<PassiveMoby>() && !_StuckInEnemy)
            {
                collision.gameObject.GetComponent<PassiveMoby>().MobyHit(_Damage);
                _StuckInEnemy = true;
                transform.SetParent(collision.transform);
                myRB.isKinematic = true;
            }
            else if (collision.gameObject.GetComponent<Gannet>())
            {
                collision.gameObject.GetComponent<Gannet>().OnExplosion();

            }
            else
            {
                Debug.Log(collision.gameObject);
                transform.SetParent(collision.transform);
                myRB.isKinematic = true;
                _Stuck = true;
                _DestroyTime = Time.time;
            }
        }
    }
}
