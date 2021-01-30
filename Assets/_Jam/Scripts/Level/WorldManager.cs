using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Debug")]
    public BiomeSettings debugBiome;

    [Header("Settings")]
    private int biomeId = 0;
    public int[] biomeMeters = new int[4];
    public LevelChunk introChunk;
    public LevelChunk finalChunk;
    public ParallaxBackgroundController background;
    public LevelChunkController level;
    public ForegroundController foreground;
    public WeatherController weather;
    public BiomeSettings[] biomeSettings;
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(player.transform.position.x > biomeMeters[biomeId])
        {
            biomeId++;
            if(biomeId >= 4)
            {
                level.SpawnFinalChunk(finalChunk);
            } else
            {
                SetBiome(biomeSettings[biomeId]);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //SetBiome(debugBiome);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //level.SpawnFinalChunk(finalChunk);
        }
    }

    private void SetBiome(BiomeSettings biome, bool skipTransition = false)
    {
        background.SetBiome(biome, skipTransition);
        level.SetBiome(biome);
        foreground.SetBiome(biome);
        weather.SetBiome(biome);

        if(biome.mergeChunk != null)
            level.introChunks.Enqueue(biome.mergeChunk);

#pragma warning disable CS0618
        player.dustParticles.startColor = biome.dustColor;
#pragma warning restore CS0618
    }

    private void Start()
    {
        level.introChunks.Enqueue(introChunk);
        level.introChunks.Enqueue(introChunk);
        level.introChunks.Enqueue(introChunk);
        SetBiome(biomeSettings[0], true);
    }
}
