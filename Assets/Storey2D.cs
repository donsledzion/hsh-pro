using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Walls2D;
using static Selector2D;

public class Storey2D : MonoBehaviour
{
    [SerializeField] GameObject _wall2DPrefab;
    [SerializeField] GameObject _floor2DPrefab;
    [SerializeField] Storey _storeyReference;
    [SerializeField] List<WallOnCanvas> _wallsOnCanvas = new List<WallOnCanvas>();
    [SerializeField] List<Wall2D> _walls2D = new List<Wall2D>();
    [SerializeField] List<FloorSectionDrawing2D> _floors2D = new List<FloorSectionDrawing2D>();
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

    public void AddWallToStorey(Wall wall)
    {
        Debug.Log("wall2DPrefab: " + _wall2DPrefab.name);
        Debug.Log("GO.transform: " + gameObject.transform.position.ToString());

        GameObject wallObject = Instantiate(_wall2DPrefab, gameObject.transform);
        
        Wall2D wall2D = wallObject.GetComponent<Wall2D>();
        _walls2D.Add(wall2D);
        wall2D.DrawOnCanvas(wall);
    }

    public void AddFloorToStorey(FloorSection2D floor)
    {
        GameObject floorObject = Instantiate(_floor2DPrefab, gameObject.transform);
        FloorSectionDrawing2D floorSection = floorObject.GetComponent<FloorSectionDrawing2D>();
        _floors2D.Add(floorSection);
        floorSection.Draw(floor);
    }


    public void SetThickness(float thickness)
    {
        foreach (WallOnCanvas wall in _wallsOnCanvas)
            wall.SetThickness(thickness);
    }

    void ClearWallsOnCanvas()
    {
        foreach (WallOnCanvas wall in WallsOnCanvas)
            Destroy(wall.gameObject);
        _wallsOnCanvas.Clear();
    }

    void ClearFloorsOnCanvas()
    {
        foreach(FloorSectionDrawing2D floor in _floors2D)
            Destroy(floor.gameObject);
        _floors2D.Clear();
    }

    void ClearWalls2D()
    {
        foreach (Wall2D wall in _walls2D)
            Destroy(wall.gameObject);
        _walls2D.Clear();
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
        ClearWallsOnCanvas();
        ClearFloorsOnCanvas();
        ClearWalls2D();
        ClearLabels();
    }

    public List<Image> CollectAllImages()
    {
        return new List<Image>(GetComponentsInChildren<Image>());
    }

    public void SetOpacity(float opacity)
    {
        foreach (Image image in CollectAllImages())
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);   
        }
    }
}
