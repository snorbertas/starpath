using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool LoadedFromMenu = false;
    public string gameplayScene = "norb3";
    private bool loading = false;
    public void PressedPlay()
    {
        if (loading) return;
        loading = true;
        LoadedFromMenu = true;
        SceneManager.LoadScene(gameplayScene);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            PressedPlay();
        }
    }
}
