using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    [Range(-0.1f, 0.1f)] 
    public float oceanRadiusOffset = 0f; 
    public NoiseLayer[] noiseLayers; 

    [System.Serializable]   
    public class NoiseLayer
    {
        public bool enable = true;
        public bool useFirstLayerAsMask; 
        public NoiseSettings noiseSettings;
    }
}
