using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkController : MonoBehaviour
{
    private ForegroundController foreground;
    public BiomeSettings currentBiome;
    public PlayerController player;
    public List<LevelChunk> spawnedChunks = new List<LevelChunk>();

    private void Awake()
    {
        foreground = FindObjectOfType<ForegroundController>();
    }

    private void Update()
    {
        if (currentBiome == null) return;
        UpdateChunks();
    }

    public Queue<LevelChunk> introChunks = new Queue<LevelChunk>();
    private Color lastGroundColor;

    private void UpdateChunks()
    {
        // Special case: spawn the initial chunks
        if(spawnedChunks.Count == 0)
        {
            SpawnChunk(introChunks.Dequeue(), null);
            SpawnChunk(introChunks.Dequeue(), spawnedChunks[0]);
        }

        // Check if we need to despawn last chunk to spawn the new one
        else if(!finalChunk)
        {
            var playerX = player.transform.position.x;
            var oldestChunk = spawnedChunks[0];
            var oldestChunkEnd = oldestChunk.chunkEnd.transform.position.x;
            var nextChunkEnd = spawnedChunks[1].chunkEnd.transform.position.x;
            var midway = (oldestChunkEnd + nextChunkEnd) / 2;

            // Chunk no longer visible
            if(playerX > midway)
            {
                // Destroy
                spawnedChunks.RemoveAt(0);
                Destroy(oldestChunk.gameObject);

                // Spawn new one
                var chunk = introChunks.Count > 0 ? introChunks.Dequeue() : currentBiome.chunks[Random.Range(0, currentBiome.chunks.Length)];
                var spawned = SpawnChunk(chunk, spawnedChunks[spawnedChunks.Count - 1]);
                var mergeChunk = spawned as MergeChunk;
                if (mergeChunk) mergeChunk.SetOldColor(lastGroundColor);
            }
        }
    }

    private bool finalChunk = false;
    public void SpawnFinalChunk(LevelChunk chunk)
    {
        finalChunk = true;
        SpawnChunk(chunk, spawnedChunks[spawnedChunks.Count - 1]);
    }

    private LevelChunk SpawnChunk(LevelChunk chunk, LevelChunk prevChunk)
    {
        var spawnedChunk = Instantiate(chunk, transform);
        var startPos = spawnedChunk.chunkStart.transform.position;
        var endPos = prevChunk ? prevChunk.chunkEnd.transform.position : Vector3.zero;
        var moveBy = prevChunk ? endPos - startPos : Vector3.zero;
        spawnedChunk.transform.position += moveBy;
        spawnedChunks.Add(spawnedChunk);

        // Also spawn foreground object on the chunk
        foreground.SpawnRandomForegroundObject(spawnedChunk);

        // And change color according to biome settings
        spawnedChunk.SetColor(currentBiome.groundColor);
        return spawnedChunk;
    }

    public void SetBiome(BiomeSettings biome)
    {
        if (currentBiome != null)
            lastGroundColor = currentBiome.groundColor;
         currentBiome = biome;
    }
}
