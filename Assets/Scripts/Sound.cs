using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    public AudioSource winSound;
    public AudioSource loseSound;
    public AudioSource scoreSound;
    public AudioSource bumpSound;

    public void playWinSound()
    {
        winSound.Play();
    }

    public void playLoseSound()
    {
        loseSound.Play();
    }

    public void playScoreSound()
    {
        scoreSound.Play();
    }

    public void playBumpSound()
    {
        bumpSound.Play();
    }
}
