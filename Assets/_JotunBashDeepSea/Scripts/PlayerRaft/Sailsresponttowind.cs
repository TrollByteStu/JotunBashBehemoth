using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailsresponttowind : MonoBehaviour
{
    public GameObject SailWindLow;
    public GameObject SailWindMedium;
    public GameObject SailWindHigh;

    private AudioSource myAS;

    private float windForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        windForce = GameController.Instance.gcWeather.sliderWind;
        if (windForce < 0.6f)
        { // low wind
            SailWindLow.SetActive(true);
            SailWindMedium.SetActive(false);
            SailWindHigh.SetActive(false);
        } else if (windForce < 0.8f )
        { // medium wind
            SailWindLow.SetActive(false);
            SailWindMedium.SetActive(true);
            SailWindHigh.SetActive(false);
        } else { // high wind
            SailWindLow.SetActive(false);
            SailWindMedium.SetActive(false);
            SailWindHigh.SetActive(true);
        }
        myAS.volume = windForce;

        myAS.pitch = (windForce * 0.5F)+0.75f;
    }
}
