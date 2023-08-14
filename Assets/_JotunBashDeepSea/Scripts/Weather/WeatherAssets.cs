using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAssets : MonoBehaviour
{
    public Material WaveMaterial;
    public float WaveMin = 0f;
    public float WaveMax = 1f;

    public WindZone windZone;
    public float WindMin = 0.5f;
    public float WindMax = 2f;

    public void UpdateWind(float wind)
    {
        WaveMaterial.SetFloat("_WaveScale", Mathf.Lerp(WaveMin,WaveMax,wind));
        windZone.windMain = Mathf.Lerp(WindMin, WindMax, wind);
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
