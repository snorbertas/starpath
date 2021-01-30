using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfPiece : MonoBehaviour
{
    public bool isColliding { get { return collisions > 0; } }
    private int collisions = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions--;
    }
}
