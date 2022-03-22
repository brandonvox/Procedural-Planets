using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] terrainMeshFilters;
    MeshFilter[] oceanMeshFilters;
    TerrainFace[] terrainFaces;


    void Initialize()
    {
        shapeGenerator.HandleShapeSettingsUpdated(shapeSettings); 
        colorGenerator.HandleColorSettingsUpdated(colorSettings); 
        if (terrainMeshFilters == null || terrainMeshFilters.Length == 0 
            || oceanMeshFilters == null || oceanMeshFilters.Length ==0)
        {
            terrainMeshFilters = new MeshFilter[6];
            oceanMeshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (terrainMeshFilters[i] == null || oceanMeshFilters[i] == null)
            {
                GameObject terrainFaceObject = new GameObject("Terrain " + i);
                terrainFaceObject.transform.parent = transform;
                terrainFaceObject.AddComponent<MeshRenderer>();
                terrainMeshFilters[i] = terrainFaceObject.AddComponent<MeshFilter>();
                terrainMeshFilters[i].sharedMesh = new Mesh();

                GameObject oceanFaceObject = new GameObject("Ocean " + i);
                oceanFaceObject.transform.parent = transform;
                oceanFaceObject.AddComponent<MeshRenderer>();
                oceanMeshFilters[i] = oceanFaceObject.AddComponent<MeshFilter>();
                oceanMeshFilters[i].sharedMesh = new Mesh(); 
            }

            terrainMeshFilters[i]. GetComponent<MeshRenderer> ().sharedMaterial = colorSettings.terrainMaterial;
            oceanMeshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.oceanMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, terrainMeshFilters[i].sharedMesh,
                oceanMeshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }


#if UNITY_EDITOR
    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }

        colorGenerator.UpdateElevationMinMaxPropertyInPlanetMaterial(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colorGenerator.UpdateTexture2DPropertyInPlanetMaterial();
    }
#endif
}
