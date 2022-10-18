using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlane : MonoBehaviour
{
    FloorSection2D _floor;
    [SerializeField] MeshFilter meshFilter;

    [SerializeField] float _overlappingOffset = 0.01f;
    Mesh mesh;
    Material _material;

    [SerializeField] float _scaleX = 1f;
    [SerializeField] float _scaleY = 1f;

    private void Awake()
    {
        mesh = meshFilter.mesh;
        _material= GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        _material.mainTextureScale = new Vector2(transform.lossyScale.x / _scaleX, transform.lossyScale.z / _scaleY);
    }

    private void MakeMeshPlane()
    {
        int[] triangles;
        string message;
        PolygonHelper.Triangulate(_floor.Points, out triangles, out message);       
        mesh.vertices = SpatializePoints(_floor.Points, _floor.TopLevel + _overlappingOffset);
        mesh.triangles = triangles;
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
    }

    public static Vector3[] SpatializePoints(Vector2[] points, float elevation)
    {
        Vector3[] points3D = new Vector3[points.Length];
        for (int i = 0; i < points3D.Length; i++)
            points3D[i] = new Vector3(points[i].x, elevation, points[i].y);
        return points3D;
    }

    public void SetParameters(FloorSection2D floor)
    {
        this._floor = floor;
    }

    public void Spatialize()
    {
        MakeMeshPlane();
    }


}
