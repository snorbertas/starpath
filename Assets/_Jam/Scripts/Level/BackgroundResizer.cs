using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;

[ExecuteInEditMode]
public class BackgroundResizer : MonoBehaviour
{
    private SpriteRenderer background;
    private Camera cam;
    private float lastCameraSize = -1f;

    private void Awake()
    {
        background = GetComponent<SpriteRenderer>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        return;
        ResizeBackground();
    }

    public void ResizeBackground()
    {
        if(lastCameraSize == cam.orthographicSize)
            return;
        lastCameraSize = cam.orthographicSize;

        // Dimensions for calculating new scale
        var bgSize = background.sprite.rect.size;
        var viewHeight = (cam.orthographicSize * 2);
        var viewHeightPixels = viewHeight * background.sprite.pixelsPerUnit;
        var viewWidth = ((float)Screen.width / Screen.height) * viewHeight;
        var viewWidthPixels = viewWidth * background.sprite.pixelsPerUnit;

        // Create and set new scale
        var newScale = Vector3.zero;
        newScale.x = viewWidthPixels / bgSize.x;
        newScale.y = viewHeightPixels / bgSize.y;
        newScale.z = 1;
        transform.localScale = newScale;
    }
}
