using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CeilingBandPlane : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
    }
    public void GenerateMesh(Vector3[] corners)
    {
        int[] triangles = new int[6];

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = corners;
        mesh.triangles = triangles;
    }
}
