using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    public Transform[] ground;
    public ChunkConnector chunkStart;
    public ChunkConnector chunkEnd;

    public void SetColor(Color color)
    {
        foreach(var grnd in ground)
        {
            if (grnd == null) continue;
            grnd.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
