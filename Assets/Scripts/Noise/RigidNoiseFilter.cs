using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter:INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings noiseSettings = new NoiseSettings();

    public RigidNoiseFilter(NoiseSettings noiseSettings)
    {
        this.noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 position)
    {
        float noiseValue = 0f;
        float frequency = noiseSettings.baseLacunarity;
        float amplitude = 1f;
        for (int i = 0; i < noiseSettings.octaves; i++)
        {
            float noiseValueOfCurrentOctave = 1 -   Mathf.Abs( noise.Evaluate(position * frequency + noiseSettings.center));
            noiseValueOfCurrentOctave *= noiseValueOfCurrentOctave;
            noiseValueOfCurrentOctave = noiseValueOfCurrentOctave * amplitude;
            noiseValue += noiseValueOfCurrentOctave;
            frequency *= noiseSettings.lacunarity;
            amplitude *= noiseSettings.persistence;
        }
        noiseValue = (noiseValue - noiseSettings.minValue) * noiseSettings.strength;
        return noiseValue;
    }
}
