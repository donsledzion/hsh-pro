using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlane : MonoBehaviour
{
    FloorSection2D _floor;
    [SerializeField] MeshFilter meshFilter;
    int[] _triangles;
    string _message;
    bool _triangulated = false;
    [SerializeField] float _overlappingOffset = 0.01f;
    Mesh mesh;
    //Material _material;
    MeshRenderer _renderer;

    [SerializeField] float _scaleX = 1f;
    [SerializeField] float _scaleY = 1f;

    private void Awake()
    {
        mesh = meshFilter.mesh;
        _renderer = GetComponent<MeshRenderer>();
        SetTilling(new Vector2(_scaleX,_scaleY));
    }

    public void SetTilling(Vector2 tiling = new Vector2())
    {
        _renderer.material.mainTextureScale = new Vector2(1 / tiling.x, 1 / tiling.y);
    }

    public void InjectMaterial(Material material)
    {
        _renderer.material = material;
        //_material = material;
    }

    public static Vector2 AdjustScale(Vector2[] points)
    {
        if(points.Length == 0) return new Vector2(0, 0);    


        Vector2 outScale = points[0];
        float minX = outScale.x;
        float minY = outScale.y;
        float maxX = outScale.x;
        float maxY = outScale.y;

        foreach (Vector2 point in points)
        {
            if (point.x < minX) minX = point.x;
            if (point.y < minY) minY = point.y;
            if (point.x > maxX) maxX = point.x;
            if (point.y > maxY) maxY = point.y;
        }
        outScale.x = Mathf.Abs(maxX - minX);
        outScale.y = Mathf.Abs(maxY - minY);


        return outScale;
    }


    public static Vector2[] PlaneRange(Vector2[] points)
    {
        Vector2[] outVectors = { new Vector2() };

        if (points.Length == 0) return outVectors;
     
        float minX = points[0].x;
        float minY = points[0].y;
        float maxX = points[0].x;
        float maxY = points[0].y;

        foreach (Vector2 point in points)
        {
            if (point.x < minX) minX = point.x;
            if (point.y < minY) minY = point.y;
            if (point.x > maxX) maxX = point.x;
            if (point.y > maxY) maxY = point.y;
        }

        Vector2[] newOutVectors = { new Vector2(minX,maxY)};

        return newOutVectors;
    }

    public static Vector2[] RotateArray(Vector2[] points, bool reverse=false)
    {
        Vector2[] outPoints = new Vector2[points.Length];

        for(int i = 0; i < points.Length-1; i++)
        {
            outPoints[i] = points[i + 1];
        }
        outPoints[outPoints.Length-1] = points[0];

        return outPoints;
    }

    public void UpdateUV()
    {
        mesh.uv = RotateArray(mesh.uv);
    }

    private void MakeMeshPlane(bool upsideDown=false)
    {        
        Debug.Log("00-GeneratinMeshPlane");
        
        if(!_triangulated)
        {
            PolygonHelper.Triangulate(_floor.Points, out _triangles, out _message);
            _triangulated = true;
        }
        
        Debug.Log("02-Polygon Triangulated");
        if (upsideDown)
        {
            mesh.vertices = SpatializePoints(_floor.Points, _floor.TopLevel);
            mesh.uv = _floor.Points;//FlatternSpatialPoints(SpatializePoints(_floor.Points, _floor.TopLevel));
            mesh.triangles = _triangles;
        }
        else
        {
            Debug.Log("03-Spatializing Points");
            mesh.vertices = SpatializePoints(_floor.Points, _floor.TopLevel + _overlappingOffset);
            Debug.Log("04-Spatialized");
            Vector2[] uv = { new Vector2() };
            //uv = _floor.Points;
            uv = _floor.Points;
            Debug.Log("05-Array reversed");

            mesh.uv = uv;//FlatternSpatialPoints(SpatializePoints(_floor.Points, _floor.TopLevel));
            mesh.triangles = _triangles;
            Debug.Log("06-Arrays assigned to mesh");
        }
        Debug.Log("07-Recalculations...");
        /*mesh.RecalculateTangents();
        mesh.RecalculateNormals();*/
        Debug.Log("08-...Done (finising)");
    }

    public static Vector3[] SpatializePoints(Vector2[] points, float elevation)
    {
        Vector3[] points3D = new Vector3[points.Length];
        for (int i = 0; i < points3D.Length; i++)
            points3D[i] = new Vector3(points[i].x, elevation, points[i].y);
        return points3D;
    }

    public static Vector2[] FlatternSpatialPoints(Vector3[] spatialPoints)
    {
        Vector2[] outPoints = new Vector2[spatialPoints.Length];
        for(int i = 0; i < spatialPoints.Length; i++)
        {
            outPoints[i] = new Vector2(spatialPoints[i].x, spatialPoints[i].y);
        }
        return outPoints;
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
