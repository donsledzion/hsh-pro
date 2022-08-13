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

    public List<WallOnCanvas> WallsOnCanvas { get { return _wallsOnCanvas; } }

    public Transform LabelsContainer { get { return _labelsContainer; } }

    public Vector2[]Points
    {
        get
        {
            List<Vector2> points = new List<Vector2>();
            foreach(WallOnCanvas wallOnCanvas in _wallsOnCanvas)
            {
                foreach(Vector2 point in wallOnCanvas.Points)
                {
                    points.Add(point);
                }
            }
            return points.ToArray();
        }
    }

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

    void ClearWalls()
    {
        foreach (WallOnCanvas wall in WallsOnCanvas)
            Destroy(wall.gameObject);
        _wallsOnCanvas.Clear();
    }

    void ClearLabels()
    {
        foreach(PointLabel label in LabelsContainer.GetComponentsInChildren<PointLabel>())
        {
            Destroy(label.gameObject);
        }
    }

    public void ClearStorey2D()
    {
        ClearWalls();
        ClearLabels();
    }
}
