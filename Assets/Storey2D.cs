using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storey2D : MonoBehaviour
{
    [SerializeField] GameObject _wall2DPrefab;
    [SerializeField] Storey _storeyReference;
    [SerializeField] List<WallOnCanvas> _wallsOnCanvas = new List<WallOnCanvas>();
    [SerializeField] Transform _labelsContainer;

    public Storey StoreyReference
    {
        get { return _storeyReference; }
        set { _storeyReference = value; }
    }

    public Transform LabelsContainer { get { return _labelsContainer; } }

    public void AddWallToStorey(Vector2[] points)
    {
        GameObject wall = Instantiate(_wall2DPrefab, gameObject.transform);
        WallOnCanvas wallOnCanvas = wall.GetComponent<WallOnCanvas>();
        wallOnCanvas.DrawOnCanvas(points);
        _wallsOnCanvas.Add(wallOnCanvas);        
    }

    public void SetThickness(float thickness)
    {
        foreach (WallOnCanvas wall in _wallsOnCanvas)
            wall.SetThickness(thickness);
    }
}
