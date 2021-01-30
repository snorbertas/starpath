using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTrigger : MonoBehaviour
{
    public enum EventType { FinalAccelerate, FinalJump, StarPower, IceChunk };
    public EventType eventType = EventType.FinalAccelerate;

    private void Update()
    {
        if(eventType == EventType.IceChunk)
        {
            transform.position += (new Vector3(-3, - 3) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject) return;
        var player = collision.GetComponent<PlayerController>();
        if (!player) return;
        player.TriggerEvent(eventType);

        if(eventType == EventType.StarPower || eventType == EventType.IceChunk)
        {
            Destroy(gameObject);
        }
    }
}
