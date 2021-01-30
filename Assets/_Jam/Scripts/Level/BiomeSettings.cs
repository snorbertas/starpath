using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome Settings", menuName = "GJ2021/Biome Settings", order = 1)]
public class BiomeSettings : ScriptableObject
{
    [Header("Weather")]
    public Weather weather = Weather.None;
    public enum Weather { None, Snowing, Raining };

    [Header("Parallax Bacgrkound")]
    public int backgroundZ = 0;
    public Transform brights;
    public Transform[] layers;
    public ParallaxBackgroundTransitionLayer transitionLayer;
    public MergeChunk mergeChunk;

    [Header("Level")]
    public Color dustColor;
    public Color groundColor;
    public LevelChunk[] chunks;
    public AudioManager.Sound slidingSound;

    [Header("Foreground Background")]
    public ForegroundObject[] foregroundPrefabs;
}
