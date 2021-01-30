using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotator : MonoBehaviour
{
    public float rotationRate = 1f;
    private float angleZ = 0;

    private void Awake()
    {
        angleZ = Random.Range(0, 360);
        rotationRate *= Random.Range(0.7f, 1.3f);
    }

    void Update()
    {
        angleZ += (Time.deltaTime * rotationRate);
        transform.localEulerAngles = new Vector3(0, 0, angleZ);
    }
}
