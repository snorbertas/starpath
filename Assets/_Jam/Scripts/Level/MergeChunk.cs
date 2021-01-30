using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeChunk : LevelChunk
{
    public SpriteRenderer flatMerge;

    public void SetOldColor(Color color)
    {
        flatMerge.color = color;
    }
}
