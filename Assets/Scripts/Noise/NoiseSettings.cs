using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class NoiseSettings
{
    public enum FilterType
    {
        Simple, Rigid
    }
    public FilterType filterType; 
    [Range(1, 8)]
    public int octaves = 1;
    public float baseLacunarity = 1f; 
    public float lacunarity = 2f;
    public float persistence = 0.5f;
    public float strength = 1f;
    public float minValue = 0f;
    public Vector3 center; 

}
