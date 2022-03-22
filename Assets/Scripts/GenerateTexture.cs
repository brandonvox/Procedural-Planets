using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTexture : MonoBehaviour
{
    //[SerializeField]
    Texture2D texture2D;
    public Gradient gradient;
    public Material material; 

    private void OnValidate()
    {
        Init();
        Generate();
    }
    public void Init()
    {
        texture2D = new Texture2D(50, 1, TextureFormat.ARGB32, false);   
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = material; 
    }

    public void Generate()
    {
        Color[] colors = new Color[50]; 
        for(int i = 0; i < colors.Length; i++)
        {
            colors[i] = gradient.Evaluate(i / 49f); 
        }
        texture2D.SetPixels(colors);
        texture2D.Apply();
        
        material.SetTexture("_texture2D", texture2D); 
    }
}
