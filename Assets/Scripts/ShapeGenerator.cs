using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{

    ShapeSettings shapeSettings;
    INoiseFilter[] noiseFilters;
    public ElevationMinMax elevationMinMax;

    public void HandleShapeSettingsUpdated(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        noiseFilters = new INoiseFilter[shapeSettings.noiseLayers.Length]; 
        
        for(int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.GetNoiseFilterFromNoiseSettings(shapeSettings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new ElevationMinMax(); 
    }

    public float CalculateUnscaledElevation(Vector3 positionOnUnitSphere)
    {
  
        float elevation = 0f;
        float firstLayerValue = 0f;

        if(noiseFilters.Length > 1)
        {
            firstLayerValue = noiseFilters[0].Evaluate(positionOnUnitSphere);
            if (shapeSettings.noiseLayers[0].enable)
            {
                elevation += firstLayerValue;  // += is same as = 
            }
        }

        for (int i =1; i< noiseFilters.Length; i++)
        {
            if(shapeSettings.noiseLayers[i].enable == false)
            {
                continue;
            }

            float mask = shapeSettings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1; 
            elevation += noiseFilters[i].Evaluate(positionOnUnitSphere) * mask;
        }

        elevationMinMax.UpdateMinMax(elevation); 
        return elevation; 
    }

    public float GetScaledElevation(float unscaledElevation)
    {
        float elevation = unscaledElevation;
        //float elevation = Mathf.Max(0, unscaledElevation); 
        elevation = shapeSettings.planetRadius * (1 + elevation);
        return elevation; 
    }
}
