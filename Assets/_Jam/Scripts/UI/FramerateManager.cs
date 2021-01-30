using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateManager : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = 61;
        QualitySettings.vSyncCount = 1;
    }

    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;
        var fpsLabel = "FPS: " + (int)(1 / Time.deltaTime);
        GUI.Label(new Rect(50, 50, 100, 100), fpsLabel, style: guiStyle);
    }
}
