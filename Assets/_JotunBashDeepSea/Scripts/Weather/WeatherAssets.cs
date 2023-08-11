using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAssets : MonoBehaviour
{
    public Material WaveMaterial;
    public float WaveMin = 0f;
    public float WaveMax = 1f;

    public void UpdateWind(float wind)
    {
        WaveMaterial.SetFloat("_WaveScale", Mathf.Lerp(WaveMin,WaveMax,wind));
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
