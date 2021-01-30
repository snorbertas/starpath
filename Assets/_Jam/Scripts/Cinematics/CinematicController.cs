using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    private PlayerController player;

    void Awake()
    {
        if (MenuController.LoadedFromMenu)
        {
            player = FindObjectOfType<PlayerController>();
            player.GetComponent<Rigidbody2D>().simulated = false;
            StartCoroutine(BeginCinematic());
            MenuController.LoadedFromMenu = false;
            AudioManager.PlaySound(AudioManager.Sound.Intro);
        }
        else
        {
            gameObject.SetActive(false);
            AudioManager.PlaySound(AudioManager.Sound.Music);
        }
    }

    IEnumerator BeginCinematic()
    {
        yield return new WaitForSeconds(25);
        player.GetComponent<Rigidbody2D>().simulated = true;
        yield return new WaitForSeconds(7);
        AudioManager.PlaySound(AudioManager.Sound.Music);
    }
}
