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

    public AudioSource LowWind1;
    public float LowWind1Max = 0.25f;
    public AudioSource LowWind2;
    public float LowWind2Max = 0.25f;
    public AudioSource LowWind3;
    public float LowWind3Max = 0.15f;
    public AudioSource HighWind1;
    public float HighWind1Max = 0.45f;
    public AudioSource HighWind2;
    public float HighWind2Max = 0.35f;
    public AudioSource HighWind3;
    public float HighWind3Max = 0.25f;

    private float getWind;
    private ParticleSystem.EmissionModule getEmmision;

    public ParticleSystem cloudMedium;
    public ParticleSystem CloudHeavy;

    public void UpdateWind(float wind)
    {
        WaveMaterial.SetFloat("_WaveScale", Mathf.Lerp(WaveMin,WaveMax,wind));
        windZone.windMain = Mathf.Lerp(WindMin, WindMax, wind);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    { // after testing reset material, so we dont get that damn spam in github.. lol
        WaveMaterial.SetFloat("_WaveScale", 0);
    }

    // Update is called once per frame
    void Update()
    {
        getWind = GameController.Instance.gcWeather.sliderWind;
        LowWind1.volume = Mathf.Clamp(Mathf.Sin(Time.timeSinceLevelLoad * 0.1f) * (0.75f - getWind) * LowWind1Max, 0, 1);
        LowWind2.volume = Mathf.Clamp(Mathf.Cos(Time.timeSinceLevelLoad * 0.15f) * (0.85f - getWind) * LowWind2Max, 0, 1);
        LowWind3.volume = Mathf.Clamp(Mathf.Sin(Time.timeSinceLevelLoad * 0.3f) * (0.65f - getWind) * LowWind3Max, 0, 1);
        HighWind1.volume = Mathf.Clamp(Mathf.Sin(Time.timeSinceLevelLoad * 0.11f) * (0.05f + getWind) * HighWind1Max, 0, 1);
        HighWind2.volume = Mathf.Clamp(Mathf.Cos(Time.timeSinceLevelLoad * 0.16f) * (0.15f + getWind) * HighWind2Max, 0, 1);
        HighWind3.volume = Mathf.Clamp(Mathf.Sin(Time.timeSinceLevelLoad * 0.33f) * (0.25f + getWind) * HighWind3Max, 0, 1);

        getEmmision = cloudMedium.emission;
        getEmmision.rateOverTime = getWind * 50f;
        getEmmision = CloudHeavy.emission;
        getEmmision.rateOverTime = getWind * 200f;
    }
}
