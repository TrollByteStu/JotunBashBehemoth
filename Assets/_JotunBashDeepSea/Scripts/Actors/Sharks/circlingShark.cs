using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class circlingShark : MonoBehaviour
{
    private GameObject _BoatRig;

    private Vector3 _Orbit;
    private float _Angle;

    private bool reachedSurface = false;

    public string _Species = "Shark";

    public float distance = 20f;
    private float distanceSin;
    public float speed = .1f;
    public float _UpSpeed;

    private Rigidbody myRigidBody;

    public Bait _Bait = null;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        _BoatRig = GameController.Instance.BoatRig;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _Bait = GameController.Instance.checkForBaits(_Species, transform);
        distanceSin = Mathf.Sin(Time.time) *2 + distance;
        _Angle += Time.fixedDeltaTime * speed;
        _Orbit = new Vector3(Mathf.Sin(_Angle) * distanceSin, transform.position.y + _UpSpeed, Mathf.Cos(_Angle) * distanceSin);
        transform.position = Vector3.Lerp(transform.position, _Orbit, speed);
        transform.LookAt(Vector3.Lerp(transform.position, _Orbit, speed));

        if ( !reachedSurface )
        {
            if (transform.position.y > -0.5f)
            {
                GetComponent<WateverVolumeFloater>().enabled = true;
                reachedSurface = true;
                _UpSpeed = 0;
            }
        }
    }
}
