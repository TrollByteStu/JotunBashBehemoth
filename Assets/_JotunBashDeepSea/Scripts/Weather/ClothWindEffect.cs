using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothWindEffect : MonoBehaviour
{
    public float windScale = 2f;

    private Cloth clothMod;
    private GameController mainGC;
    private WindZone mainWind;

    // Use this for initialization
    void Start()
    {
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
        clothMod = this.GetComponent<Cloth>();
        mainWind = mainGC.gcWeather.mainWind;
    }

    // Update is called once per frame
    void Update()
    {
        clothMod.externalAcceleration = mainWind.transform.forward * (mainWind.windMain * windScale) + Random.insideUnitSphere * mainWind.windTurbulence;
    }
}
