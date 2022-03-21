using UnityEngine;

public class ColorGenerator
{
    Texture2D texture2D;
    int textureResolution; 
    ColorSettings colorSettings;


    public void HandleColorSettingsUpdated(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
        textureResolution = colorSettings.textureResolution; 
        if(texture2D == null || texture2D.width != textureResolution * 2)
        {
            texture2D = new Texture2D(textureResolution * 2, 1, TextureFormat.ARGB32, false);
        }
    }

    public void UpdateElevationMinMaxPropertyInPlanetMaterial(ElevationMinMax elevationMinMax)
    {
        colorSettings.planetMaterial
            .SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateTexture2DPropertyInPlanetMaterial()
    {
        // HERE
        Color[] colors = new Color[textureResolution * 2];  // Test with color 32 later
        for(int i = 0; i < textureResolution * 2; i++)
        {
            if(i < textureResolution)
            {
                colors[i] = colorSettings.oceanGradient.Evaluate(i / (textureResolution - 1f));
            }
            else
            {
                colors[i] = colorSettings.planetGradient.Evaluate((i - textureResolution)/ (textureResolution - 1f));
            }

        }

        texture2D.SetPixels(colors);
        texture2D.Apply();
        colorSettings.planetMaterial.SetTexture("_texture2D", texture2D);
    }
}
