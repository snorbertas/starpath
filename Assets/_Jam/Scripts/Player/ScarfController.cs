using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfController : MonoBehaviour
{
    bool collide = true;
    public int activePieces = 0;
    public LineRenderer innerScarf;
    public Transform[] scarfPiece;
    public ScarfPiece collidingPiece;
    private LineRenderer lineRenderer;
    public float velocity = 1f;
    public float gapDistance = 1f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        var moveBy = new Vector3(0, -velocity * Time.deltaTime, 0);
        if (collide && collidingPiece.isColliding) moveBy = Vector3.zero;
        for(int i = 1; i < scarfPiece.Length; i++)
        {
            moveBy *= (1 + (i / 10));
            var prevPiece = scarfPiece[i - 1];
            var thisPiece = scarfPiece[i];
            thisPiece.position += moveBy;

            // close the gap
            var dir = thisPiece.position - prevPiece.position;
            var distance = dir.magnitude;
            if(distance > gapDistance)
            {
                thisPiece.position = prevPiece.position + (dir.normalized * gapDistance);
            }
        }

        UpdateScarfVisual();
    }

    private void UpdateScarfVisual()
    {
        lineRenderer.positionCount = activePieces + 1;
        innerScarf.positionCount = activePieces + 1;
        for (int i = 0; i < scarfPiece.Length; i++)
        {
            var pos = scarfPiece[i].position;
            pos.z = 0;

            if (i > activePieces) continue;
            lineRenderer.SetPosition(i, pos);
            innerScarf.SetPosition(i, pos);
        }
    }
}
