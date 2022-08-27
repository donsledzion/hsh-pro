using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CeilingSection : MonoBehaviour
{
    //public CeilingParameters Parameters { get; private set; }
    Ceiling _ceiling;
    [SerializeField] MeshFilter meshFilter;

    [SerializeField] float _overlappingOffset = 0.01f;
    Mesh mesh;

    private void Awake()
    {
        mesh = meshFilter.mesh;
    }


    private void MakeMeshData()
    {
        Debug.Log("Creating mesh data...");
        int[] triangles;
        string message;
        PolygonHelper.Triangulate(_ceiling.Points, out triangles, out message);
        Debug.Log("Triangles count: " + triangles.Length);
        Debug.Log("Messsage: " + message);
        mesh.vertices = SpatializePoints(_ceiling.Points, _ceiling.TopLevel + _overlappingOffset);
        foreach (Vector3 point in mesh.vertices)
            Debug.Log("Point: " + point.ToString());
        mesh.triangles = triangles;
    }

    public static Vector3[] SpatializePoints(Vector2[] points, float elevation)
    {
        Vector3[] points3D = new Vector3[points.Length];
        for(int i = 0; i < points3D.Length; i++)
            points3D[i] = new Vector3(points[i].x,elevation,points[i].y);
        return points3D;
    }

    public void SetParameters(Ceiling ceiling)
    {
        this._ceiling = ceiling;
        /*Parameters._thicknes = ceiling.Thickness;
        Parameters._topLevel = ceiling.TopLevel;
        Parameters._points = ceiling.Points;*/
    }

    public void Spatialize()
    {
        MakeMeshData();
    }
}

public class CeilingParameters
{
    public float _thicknes;
    public float _topLevel;
    public Vector2[] _points;
}
