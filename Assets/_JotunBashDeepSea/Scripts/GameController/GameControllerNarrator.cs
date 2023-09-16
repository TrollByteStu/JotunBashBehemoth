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
    public AudioClip[] ReactionLumber;
    public AudioClip[] ReactionFruitNinjaRaft;

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
                chooseClip = VoiceoverIntro[Random.Range(1, VoiceoverIntro.Length)];
                break;
            case "Gannet":
                chooseClip = VoiceoverGannet[Random.Range(1, VoiceoverGannet.Length)];
                break;
            case "Plushie":
                chooseClip = VoiceoverPlushie[Random.Range(1, VoiceoverPlushie.Length)];
                break;
            case "GreatWhite":
                chooseClip = VoiceoverGreatWhite[Random.Range(1, VoiceoverGreatWhite.Length)];
                break;
            case "MobyDick":
                chooseClip = VoiceoverMobyDick[Random.Range(1, VoiceoverMobyDick.Length)];
                break;
            case "ShotGunPickup":
                chooseClip = ReactionShotGunPickup[Random.Range(1, ReactionShotGunPickup.Length)];
                break;
            case "HarpoonPickup":
                chooseClip = ReactionHarpoonPickup[Random.Range(1, ReactionHarpoonPickup.Length)];
                break;
            case "BrineDrink":
                chooseClip = ReactionBrineDrink[Random.Range(1, ReactionBrineDrink.Length)];
                break;
            case "ForgetfulDrink":
                chooseClip = ReactionForgetfulDrink[Random.Range(1, ReactionForgetfulDrink.Length)];
                break;
            case "FruitNinja":
                chooseClip = ReactionFruitNinja[Random.Range(1, ReactionFruitNinja.Length)];
                break;
            case "Lumber":
                chooseClip = ReactionLumber[Random.Range(1, ReactionLumber.Length)];
                break;
            case "FruitNinjaRaft":
                chooseClip = ReactionFruitNinjaRaft[Random.Range(1, ReactionFruitNinjaRaft.Length)];
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
