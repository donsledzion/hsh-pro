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

    void Start()
    {        
        
        


        width = DefaultSettings.ins.LoadBareringWallWidth;
        floorLvl = GameManager.ins.Building.CurrentStorey.Elevation;
        ceilLvl = GameManager.ins.Building.CurrentStorey.Elevation + GameManager.ins.Building.CurrentStorey.Height;
    }

    public void GenerateSectionMesh(WallSection wallSection)
    {
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
        List<Vector3> verticesList = new List<Vector3>();
        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x - width / 2, floorLvl, wallSection.StartPoint.Position.y));
        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x + width / 2, floorLvl, wallSection.StartPoint.Position.y));

        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x - width / 2, floorLvl, wallSection.EndPoint.Position.y));
        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x + width / 2, floorLvl, wallSection.EndPoint.Position.y));

        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x - width / 2, ceilLvl, wallSection.StartPoint.Position.y));
        verticesList.Add(new Vector3(wallSection.StartPoint.Position.x + width / 2, ceilLvl, wallSection.StartPoint.Position.y));

        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x - width / 2, ceilLvl, wallSection.EndPoint.Position.y));
        verticesList.Add(new Vector3(wallSection.EndPoint.Position.x + width / 2, ceilLvl, wallSection.EndPoint.Position.y));

        return verticesList.ToArray();
    }

    int[] GenerateTriangles()
    {
        int[] triangles = { 0, 1, 4, 1, 5, 4, 1, 3, 5, 5, 3, 7, 4, 7, 6, 7, 4, 5, 0, 3, 1, 0, 2, 3, 0, 2, 6, 0, 6, 4, 2, 7, 6, 2, 3, 7 };
        return triangles;

    }
}
