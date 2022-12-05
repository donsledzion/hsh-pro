using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Walls2D;
using static Selector2D;

public class WallBuilder : DrawWithLines
{
    List<WallSection> _wallSections = new List<WallSection>();

    [SerializeField] WallSectionSnapClosePoint _wallSectionSnapClosePoint;
    [SerializeField] GameObject _gridDot;

    private void OnEnable()
    {
        IsDrawing = true;
        _wallSectionSnapClosePoint.AllowJambs = false;
    }

    protected override void OnDisable()
    {
        IsDrawing = false;
        base.OnDisable();
        _wallSectionSnapClosePoint.AllowJambs = true;
    }

    public void AddWallSection()
    {
        /*Vector2[] allPoints = _drawing2DController.LinePoints;
        int allPointsCount = allPoints.Length;
        if (allPointsCount < 2) return;
        Vector2[] sectionPoints = { allPoints[allPointsCount - 2], allPoints[allPointsCount - 1] };
        WallSection section = new SectionStraight(sectionPoints);        
        _wallSections.Add(section);*/
    }

    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        if (_drawing2DController.LinePointsCount < 2) return;
        IsDrawing = false;
        
        _drawing2DController.ClearLiveLine();
        Wall wall = _drawing2DController.ApplyWallToBuilding();
        _drawing2DController.StoreWall(wall);
        //=============================================================
        CheckForLinesToBreak();
        //=============================================================
        _drawing2DController.ClearCurrentLine();
        
    }

    private void CheckForLinesToBreak()
    {
        Vector2[] storeyPoints = GameManager.ins.Building.CurrentStorey.WallSectionPoints;
        List<WallSection> sections = GameManager.ins.Building.CurrentStorey.SectionsOfType(typeof(SectionStraight));
        foreach(Vector2 point in storeyPoints)
        {
            foreach(WallSection section in sections)
            {
                if((ClosestSection(point,10f) == section) && section.PointAwayFromEdges(point))
                {
                    /*GameObject dotInstance = Instantiate(_gridDot, transform);
                    dotInstance.transform.localPosition = point;
                    dotInstance.transform.localScale = Vector3.one * 5f;
                    dotInstance.GetComponent<Image>().color = Color.red;*/
                    if (section.SplitSection(point))
                        CheckForLinesToBreak();
                    return;
                }
            }
        }
        Drawing2DController.ins.RedrawCurrentStorey();
    }

    protected override void HandleClick()
    {
        AddWallSection();
    }

    public WallSection ClosestSection(Vector2 point, float minDistanceFromEdge = 0f)
    {
        float lineSnapDistance = 1f;
        List<LineSection> segments = new List<LineSection>();
        List<Wall> walls = GameManager.ins.Building.CurrentStorey.Walls;
        WallSection closestSection = null;
        float closestDistance = lineSnapDistance;
        foreach (Wall wall in walls)
        {
            foreach (WallSection section in wall.WallSections)
            {
                LineSection lSection = new LineSection(section);
                segments.Add(lSection);
                float distance = MathHelpers.PointToLineDistance(lSection.Start, lSection.End, point);
                bool pointWithinSection = MathHelpers.DoesPointCastsOnLine(lSection.Start, lSection.End, point);
                if ((distance < closestDistance) 
                    && pointWithinSection 
                    && (((point - section.StartPoint.Position).magnitude >= minDistanceFromEdge)
                        && (point - section.EndPoint.Position).magnitude >= minDistanceFromEdge))
                {
                    closestDistance = distance;
                    closestSection = section;
                }
            }
        }
        if (closestSection != null)
            Debug.Log("Closest section wall: " + closestSection.Wall);
        return closestSection;
    }
}
