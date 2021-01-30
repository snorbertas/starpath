using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundController : MonoBehaviour
{
    public BiomeSettings currentBiome;
    public float multiplierX;
    public Camera cam;
    public PlayerController player;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerController>();
    }

    public void SpawnRandomForegroundObject(LevelChunk chunk)
    {
        if (currentBiome == null || currentBiome.foregroundPrefabs.Length == 0) return;
        var index = Random.Range(0, currentBiome.foregroundPrefabs.Length);
        var foregroundObject = Instantiate(currentBiome.foregroundPrefabs[index], chunk.chunkEnd.transform);
        foregroundObject.transform.eulerAngles = Vector3.zero;
        var moveBy = chunk.chunkEnd.transform.position - foregroundObject.spawnConnector.transform.position;
        foregroundObject.transform.position += moveBy;
    }

    public void SetBiome(BiomeSettings biome)
    {
        currentBiome = biome;
    }
}
