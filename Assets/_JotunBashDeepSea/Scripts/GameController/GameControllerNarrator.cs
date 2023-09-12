using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerNarrator : MonoBehaviour
{
    public bool doIntro = true;

    public AudioClip[] VoiceoverIntro;
    public AudioClip[] VoiceoverGannet;
    public AudioClip[] VoiceoverPlushie;
    public AudioClip[] VoiceoverGreatWhite;
    public AudioClip[] VoiceoverMobyDick;

    public AudioClip[] ReactionShotGunPickup;
    public AudioClip[] ReactionHarpoonPickup;
    public AudioClip[] ReactionBrineDrink;
    public AudioClip[] ReactionForgetfulDrink;
    public AudioClip[] ReactionFruitNinja;

    public List<string> alreadyPlayed;
    public List<string> waitingToPlay;

    private AudioSource myAS;

    public void Tell(string story)
    {
        if (alreadyPlayed.Contains(story)) return;
        if (waitingToPlay.Contains(story)) return;
        waitingToPlay.Add(story);
    }

    public void TellNow(string story)
    {
        if (alreadyPlayed.Contains(story)) return;
        if (waitingToPlay.Contains(story)) return;
        if (myAS.isPlaying) return;
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
            case "Plushie":
                chooseClip = VoiceoverPlushie[Random.Range(1, VoiceoverPlushie.Length) - 1];
                break;
            case "GreatWhite":
                chooseClip = VoiceoverGreatWhite[Random.Range(1, VoiceoverGreatWhite.Length) - 1];
                break;
            case "MobyDick":
                chooseClip = VoiceoverMobyDick[Random.Range(1, VoiceoverMobyDick.Length) - 1];
                break;
            case "ShotGunPickup":
                chooseClip = ReactionShotGunPickup[Random.Range(1, ReactionShotGunPickup.Length) - 1];
                break;
            case "HarpoonPickup":
                chooseClip = ReactionHarpoonPickup[Random.Range(1, ReactionHarpoonPickup.Length) - 1];
                break;
            case "BrineDrink":
                chooseClip = ReactionBrineDrink[Random.Range(1, ReactionBrineDrink.Length) - 1];
                break;
            case "ForgetfulDrink":
                chooseClip = ReactionForgetfulDrink[Random.Range(1, ReactionForgetfulDrink.Length) - 1];
                break;
            case "FruitNinja":
                chooseClip = ReactionFruitNinja[Random.Range(1, ReactionFruitNinja.Length) - 1];
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
        if (doIntro ) Tell("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        if (!myAS.isPlaying && waitingToPlay.Count > 0) playNext();
    }
}
