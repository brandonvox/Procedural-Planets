using UnityEngine;


[System.Serializable]    
public class ColorGenerator
{
    [SerializeField]  
    public Texture2D texture2D;
    int textureResolution; 
    ColorSettings colorSettings;

    public void HandleColorSettingsUpdated(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
        textureResolution = colorSettings.textureResolution; 
        if(texture2D == null || texture2D.width != textureResolution)
        {
            texture2D = new Texture2D(textureResolution, 1, TextureFormat.ARGB32, false);
        }
    }

    public void UpdateElevationMinMaxPropertyInPlanetMaterial(ElevationMinMax elevationMinMax)
    {
        colorSettings.terrainMaterial
            .SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateTexture2DPropertyInPlanetMaterial()
    {
        // HERE
        Color[] colors = new Color[textureResolution];  // Test with color 32 later
        for (int i = 0; i < textureResolution; i++)
        {     
                colors[i] = colorSettings.planetGradient
                .Evaluate(i/ (textureResolution - 1f));
            

        }

        texture2D.SetPixels(colors);
        texture2D.Apply();
        colorSettings.terrainMaterial.SetTexture("_texture2D", texture2D);
    }
}
