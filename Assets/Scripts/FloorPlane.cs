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
    MeshRenderer _renderer;

    [SerializeField] float _scaleX = 1f;
    [SerializeField] float _scaleY = 1f;

    public FloorSection2D Floor { get { return _floor; } }

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
        if(!_triangulated)
        {
            PolygonHelper.Triangulate(_floor.Points, out _triangles, out _message);
            _triangulated = true;
        }
        
        if (upsideDown)
        {
            mesh.vertices = SpatializePoints(_floor.Points, _floor.TopLevel);
            mesh.uv = _floor.Points;
            mesh.triangles = _triangles;
        }
        else
        {
            mesh.vertices = SpatializePoints(_floor.Points, _floor.TopLevel + _overlappingOffset);
            Vector2[] uv = { new Vector2() };
            uv = _floor.Points;

            mesh.uv = uv;
            mesh.triangles = _triangles;
        }
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

    internal void DrawMaterial()
    {
        if(Floor.MaterialName != null && Floor.MaterialName != "")
        {
            Debug.Log("Found material: " + Floor.MaterialName);
            AssetBundle bundle = AssetBundleLoader.ins.FloorSurfacesBundle.LoadBundle();
            if(bundle != null)
            {
                ScriptableObjectsController item = bundle.LoadAsset(Floor.MaterialName) as ScriptableObjectsController;
                Material material = item.material;
                _renderer.material = material;
                SetTilling(new Vector2(item.tiling_x,item.tiling_y));
            }
            else
            {
                Debug.LogWarning("Bundle (floors) not loaded!");
                return;
            }
        }
        else
        {
            Debug.LogWarning("Material not assigned");
            return;
        }
        
    }
}
