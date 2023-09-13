using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleTalking : MonoBehaviour
{

    public AudioClip[] narratorClips;
    public AudioClip[] peopleTalkingClips;

    private AudioSource myAS;

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        myAS.clip = narratorClips[Random.Range(1, narratorClips.Length)];
        myAS.Play();
        myAS.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if ( !myAS.isPlaying )
        {
            myAS.volume = 0.75f;
            myAS.clip = peopleTalkingClips[Random.Range(1, peopleTalkingClips.Length)];
            myAS.Play();
        }
    }
}
