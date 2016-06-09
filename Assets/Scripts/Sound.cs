using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    /// <summary>
    /// The win sound.
    /// </summary>
    public AudioSource winSound;

    /// <summary>
    /// The lose sound.
    /// </summary>
    public AudioSource loseSound;

    /// <summary>
    /// The score sound.
    /// </summary>
    public AudioSource scoreSound;

    /// <summary>
    /// The bump sound.
    /// </summary>
    public AudioSource bumpSound;


    /// <summary>
    /// Play the win sound.
    /// </summary>
    public void playWinSound()
    {
        winSound.Play();
    }

    /// <summary>
    /// Play the lose sound.
    /// </summary>
    public void playLoseSound()
    {
        loseSound.Play();
    }

    /// <summary>
    /// Play the score sound.
    /// </summary>
    public void playScoreSound()
    {
        scoreSound.Play();
    }

    /// <summary>
    /// Play the bump sound.
    /// </summary>
    public void playBumpSound()
    {
        bumpSound.Play();
    }
}
