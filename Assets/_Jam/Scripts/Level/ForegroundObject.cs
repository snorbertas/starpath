using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundObject : MonoBehaviour
{
    public float xVelocity = 10f;
    public ChunkConnector spawnConnector;

    private void Update()
    {
        transform.localPosition -= new Vector3(xVelocity * Time.deltaTime, 0, 0);
    }
}
