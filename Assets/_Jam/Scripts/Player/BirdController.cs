using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject[] birds;

    void Update()
    {
        transform.position += (new Vector3(-5, 1, 0) * Time.deltaTime);
    }

    private void Start()
    {
        foreach(var bird in birds)
            StartCoroutine(Offset(bird));
    }

    IEnumerator Offset(GameObject bird)
    {
        yield return new WaitForSeconds(Random.Range(0, 1));
        bird.SetActive(true);
    }
}
