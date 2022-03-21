using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    [SerializeField]
    public Material planetMaterial;
    public Gradient planetGradient;
    public Gradient oceanGradient;
    [Range(10, 200)]
    public int textureResolution; 
}
