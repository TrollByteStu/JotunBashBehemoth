using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerWeather : MonoBehaviour
{
    public WeatherAssets myWeatherAssets;

    // These slider work from 0(calm day at sea) to 1(Massive biblical storm) 
    public float sliderWind = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderWind = Mathf.Clamp( sliderWind + Time.deltaTime * 0.001f, 0f, 1f);
        myWeatherAssets.UpdateWind( sliderWind );
    }
}
