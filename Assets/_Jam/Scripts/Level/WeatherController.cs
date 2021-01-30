using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rain;
    public ParticleSystem snow;
    public BiomeSettings currentBiome;

    public void SetBiome(BiomeSettings biome)
    {
        currentBiome = biome;

        TurnOffAll();
        if (currentBiome.weather == BiomeSettings.Weather.None)
        {
            // nothing to do
        }

        else if(currentBiome.weather == BiomeSettings.Weather.Raining)
        {
            TurnOnRain();
        }

        else if(currentBiome.weather == BiomeSettings.Weather.Snowing)
        {
            TurnOnSnow();
        }
    }

    private void TurnOffAll()
    {
        rain.Stop();
        snow.Stop();
    }

    private void TurnOnRain()
    {
        rain.Play();
    }

    private void TurnOnSnow()
    {
        snow.Play();
    }
}
