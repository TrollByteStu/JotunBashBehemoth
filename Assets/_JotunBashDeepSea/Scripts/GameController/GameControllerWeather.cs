using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerWeather : MonoBehaviour
{
    public WeatherAssets myWeatherAssets;

    public WindZone mainWind;

    // These slider work from 0(calm day at sea) to 1(Massive biblical storm) 
    public float sliderWind = 0.25f;

    // Start is called before the first frame update
    void Awake()
    {
        mainWind = GameObject.Find("WindZone").GetComponent<WindZone>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderWind = Mathf.Clamp( sliderWind + Time.deltaTime * 0.001f, 0f, 1f);
        myWeatherAssets.UpdateWind( sliderWind );
    }
}
