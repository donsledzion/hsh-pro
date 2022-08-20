using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionMeshGenerator : MonoBehaviour
{
    Mesh mesh;
    float width;
    float floorLvl;
    float ceilLvl;

    public void GenerateSectionMesh(WallSection wallSection)
    {
        width = DefaultSettings.ins.LoadBareringWallWidth;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = GenerateVertices(wallSection);
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
    }

    public void SetStoreyParams(Storey storey)
    {
        floorLvl = storey.Elevation;
        ceilLvl = (storey.Elevation + storey.Height);
    }

    Vector3[] GenerateVertices(WallSection wallSection)
    {
        float azimuth = MathHelpers.VectorAzimuthRad(wallSection.EndPoint.Position - wallSection.StartPoint.Position);
        Debug.Log("Azimuth: " + azimuth);
        List<Vector3> verticesList = new List<Vector3>();
        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x - (width / 2) * Mathf.Cos(azimuth), floorLvl, wallSection.StartPoint.Position.y + (width / 2) * Mathf.Sin(azimuth)));
        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x + (width / 2) * Mathf.Cos(azimuth), floorLvl, wallSection.StartPoint.Position.y - (width / 2) * Mathf.Sin(azimuth)));

        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x - (width / 2) * Mathf.Cos(azimuth), floorLvl, wallSection.EndPoint.Position.y + (width / 2) * Mathf.Sin(azimuth)));
        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x + (width / 2) * Mathf.Cos(azimuth), floorLvl, wallSection.EndPoint.Position.y - (width / 2) * Mathf.Sin(azimuth)));

        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x - (width / 2) * Mathf.Cos(azimuth), ceilLvl, wallSection.StartPoint.Position.y + (width / 2) * Mathf.Sin(azimuth)));
        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x + (width / 2) * Mathf.Cos(azimuth), ceilLvl, wallSection.StartPoint.Position.y - (width / 2) * Mathf.Sin(azimuth)));

        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x - (width / 2) * Mathf.Cos(azimuth), ceilLvl, wallSection.EndPoint.Position.y + (width / 2) * Mathf.Sin(azimuth)));
        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x + (width / 2) * Mathf.Cos(azimuth), ceilLvl, wallSection.EndPoint.Position.y - (width / 2) * Mathf.Sin(azimuth)));

        return verticesList.ToArray();
    }

    int[] GenerateTriangles()
    {
        int[] triangles = { 0, 1, 4, 1, 5, 4, 1, 3, 5, 5, 3, 7, 4, 7, 6, 7, 4, 5, 0, 3, 1, 0, 2, 3, 0, 2, 6, 0, 6, 4, 2, 7, 6, 2, 3, 7 };
        return triangles;

    }
}
