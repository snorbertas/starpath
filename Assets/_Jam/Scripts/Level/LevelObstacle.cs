using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (!player) return;

        player.GetHit();
    }
}
