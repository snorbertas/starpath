using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Sound { Intro, Music, SlideSnow, SlideSand, SlideForest, PowerUp, Flap, Impact };
    public AudioSource introMusic;
    public AudioSource johnMusic;
    public AudioSource slidingSnow;
    public AudioSource slidingSand;
    public AudioSource slidingForest;
    public AudioSource powerUp;
    public AudioSource flap;
    public AudioSource impact;
    private static AudioManager singleton;
    private static Sound slideSound = Sound.SlideSand;

    private void Awake()
    {
        singleton = this;
    }

    public static void SetSlideSound(Sound sound)
    {
        slideSound = sound;
    }

    public static void PlaySlideSound()
    {
        PlaySound(slideSound);
    }

    public static void PlaySound(Sound sound)
    {
        if (sound == Sound.Intro)
        {
            singleton.introMusic.Play();
        }

        else if (sound == Sound.Music)
        {
            singleton.johnMusic.Play();
        }

        else if (sound == Sound.SlideSnow)
        {
            singleton.slidingSnow.Play();
        }

        else if (sound == Sound.SlideSand)
        {
            singleton.slidingSnow.Play();
        }

        else if(sound == Sound.SlideForest)
        {
            singleton.slidingForest.Play();
        }

        else if (sound == Sound.PowerUp)
        {
            singleton.powerUp.Play();
        }

        else if (sound == Sound.Flap)
        {
            singleton.flap.Play();
        }

        else if (sound == Sound.Impact)
        {
            singleton.impact.Play();
        }
    }

    public static void StopSliding()
    {
        singleton.slidingSnow.Stop();
        singleton.slidingSand.Stop();
        singleton.slidingForest.Stop();
    }
}
