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

    public List<string> alreadyPlayed;
    public List<string> waitingToPlay;

    private AudioSource myAS;

    public void Tell(string story)
    {
        if (alreadyPlayed.Contains(story)) return;
        if (waitingToPlay.Contains(story)) return;
        waitingToPlay.Add(story);
    }

    void playNext()
    {
        AudioClip chooseClip;
        switch (waitingToPlay[0])
        {
            case "Intro":
                chooseClip = VoiceoverIntro[Random.Range(1, VoiceoverIntro.Length) - 1];
                break;
            case "Gannet":
                chooseClip = VoiceoverGannet[Random.Range(1, VoiceoverGannet.Length) - 1];
                break;
            default:
                return;
        }
        alreadyPlayed.Add(waitingToPlay[0]);
        waitingToPlay.Remove(waitingToPlay[0]);
        myAS.clip = chooseClip;
        myAS.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        Tell("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        if (!myAS.isPlaying && waitingToPlay.Count > 0) playNext();
    }
}
