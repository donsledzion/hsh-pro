using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
using static Selector2D;

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

    public Point2DInfo[] Points2DInfo
    { 
        get
        {
            List<Point2DInfo> points2DInfo = new List<Point2DInfo>();
            foreach(Wall wall in StoreyReference.Walls)
            {
                foreach (Vector2 point in wall.Points2D)
                    points2DInfo.Add(new Point2DInfo(point, wall));
            }
            return points2DInfo.ToArray();
        }
    }

    public void AddWallToStorey(/*Vector2[] points*/ Wall wall)
    {
        GameObject wallObject = Instantiate(_wall2DPrefab, gameObject.transform);
        WallOnCanvas wallOnCanvas = wallObject.GetComponent<WallOnCanvas>();
        wallOnCanvas.DrawOnCanvas(wall.Points2D);
        wallOnCanvas.SetWall(wall);
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
