using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerNarrator : MonoBehaviour
{

    public AudioClip[] VoiceoverIntro;
    public AudioClip[] VoiceoverGannet;
    public AudioClip[] VoiceoverPlushie;
    public AudioClip[] VoiceoverGreatWhite;
    public AudioClip[] VoiceoverMobyDick;

    private AudioSource myAS;

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
