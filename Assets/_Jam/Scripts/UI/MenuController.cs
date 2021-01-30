using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool LoadedFromMenu = false;
    public string gameplayScene = "norb3";
    public void PressedPlay()
    {
        LoadedFromMenu = true;
        SceneManager.LoadScene(gameplayScene);
    }
}
