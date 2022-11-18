using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CeilingPlane : MonoBehaviour
{
    Ceiling _ceiling;
    [SerializeField] bool _upsideDown;
    [SerializeField] MeshFilter meshFilter;

    [SerializeField] float _overlappingOffset = 0.01f;
    Mesh mesh;


    MeshRenderer _renderer;

    [SerializeField] float _scaleX = 100f;
    [SerializeField] float _scaleY = 100f;

    private void Awake()
    {
        mesh = meshFilter.mesh;
        _renderer = GetComponent<MeshRenderer>();
        SetTilling(new Vector2(_scaleX, _scaleY));
    }

    public void SetTilling(Vector2 tiling = new Vector2())
    {
        _renderer.material.mainTextureScale = new Vector2(1 / tiling.x, 1 / tiling.y);
    }

    private void MakeMeshPlane(bool upsideDown)
    {
        int[] triangles;
        string message;
        PolygonHelper.Triangulate(_ceiling.Points, out triangles, out message);
        if(upsideDown)
        {
            mesh.vertices = SpatializePoints(_ceiling.Points, _ceiling.TopLevel -_ceiling.Thickness);
            Array.Reverse(triangles);
            Vector2[] uv = { new Vector2() };
            uv = _ceiling.Points;

            mesh.uv = uv;
            mesh.triangles = triangles;            
        }            
        else
        {
            mesh.vertices = SpatializePoints(_ceiling.Points, _ceiling.TopLevel - _overlappingOffset);
            Vector2[] uv = { new Vector2() };
            uv = _ceiling.Points;

            mesh.uv = uv;
            mesh.triangles = triangles;
        }            
    }

    public static Vector3[] SpatializePoints(Vector2[] points, float elevation)
    {
        Vector3[] points3D = new Vector3[points.Length];
        for (int i = 0; i < points3D.Length; i++)
            points3D[i] = new Vector3(points[i].x, elevation, points[i].y);
        return points3D;
    }

    public void SetParameters(Ceiling ceiling)
    {
        this._ceiling = ceiling;
    }

    public void Spatialize()
    {
        MakeMeshPlane(_upsideDown);
    }
}