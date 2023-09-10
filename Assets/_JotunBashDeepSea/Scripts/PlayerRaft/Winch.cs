using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winch : MonoBehaviour
{

    public float PlaySound = 0f;

    private AudioSource myAS;

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlaySound > 0 && !myAS.isPlaying) myAS.Play();
        if (PlaySound <= 0 && myAS.isPlaying) myAS.Stop();
        PlaySound -= Time.deltaTime;
    }
}
