using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plushie : MonoBehaviour
{

    public int MaxTouchZones = 5;
    private int lastTouch = 0;

    public bool beingHeld = false;
    public bool beenPickedUp = false;

    private Rigidbody myRigidBody;

    private void ImproveWeather()
    {
        lastTouch = 0;
        GameController.Instance.gcWeather.sliderWind -= 0.1f;
    }
    public void PlushieTouchReportTouch(int number)
    {
        if ( number == 1)
        {
            lastTouch = 1;
            return;
        }
        if ( number == lastTouch +1 )
        {
            lastTouch = number;

        } else {
            lastTouch = 0;

        }
    }

    public void eventSelect()
    {
        beingHeld = true;
        beenPickedUp = true;
    }

    public void eventUnSelect()
    {
        myRigidBody.isKinematic = false;
        transform.SetParent(null);
        GameController.Instance.gcWeather.sliderWind += 0.1f;
        Destroy(gameObject, 10f);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
