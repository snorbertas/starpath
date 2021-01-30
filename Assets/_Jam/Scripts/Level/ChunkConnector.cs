using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkConnector : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(0, 0, 50f), 0.02f);
    }
}
