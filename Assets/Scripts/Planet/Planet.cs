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
    [SerializeField, HideInInspector]
    MeshFilter[] oceanMeshFilters;
    TerrainFace[] terrainFaces;

    public enum RenderFace
    {
        Top, Down, Left, Right, Front, Back, All
    }
    public RenderFace renderFace; 

    void Initialize()
    {
        shapeGenerator.HandleShapeSettingsUpdated(shapeSettings); 
        colorGenerator.HandleColorSettingsUpdated(colorSettings); 
        if (terrainMeshFilters == null || terrainMeshFilters.Length == 0)
        {
            terrainMeshFilters = new MeshFilter[6];
        }
        if (oceanMeshFilters == null || oceanMeshFilters.Length == 0)
        {
            oceanMeshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (terrainMeshFilters[i] == null )
            {
                GameObject terrainFaceObject = new GameObject("Terrain " + i);
                terrainFaceObject.transform.parent = transform;
                terrainFaceObject.AddComponent<MeshRenderer>();
                terrainMeshFilters[i] = terrainFaceObject.AddComponent<MeshFilter>();
                terrainMeshFilters[i].sharedMesh = new Mesh();
            }

            if(oceanMeshFilters[i] == null)
            {
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

            bool doRendering = renderFace == RenderFace.All  || (int)renderFace == i;
            terrainMeshFilters[i].gameObject.SetActive(doRendering);
            oceanMeshFilters[i].gameObject.SetActive(doRendering);
        }
    }


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
        for (int i = 0; i < 6; i++)
        {
            if (terrainMeshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colorGenerator.UpdateElevationMinMaxPropertyInPlanetMaterial(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colorGenerator.UpdateTexture2DPropertyInPlanetMaterial();
    }
}
