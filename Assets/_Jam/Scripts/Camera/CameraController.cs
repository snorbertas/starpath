using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cinematicTarget;
    public float dampTime = 0.15f;
    private Vector2 camVelocity = Vector3.zero;
    public float lerpX = 0.5f;
    public float lerpY = 0.5f;
    public float offsetX = -1f;
    public float offsetY = 0f;
    public PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        var destination = Vector2.SmoothDamp(
            transform.position,
            player.transform.position + new Vector3(offsetX, offsetY, 0),
            ref camVelocity,
            dampTime);

        transform.position = new Vector3(destination.x, destination.y, -10);
    }
}
