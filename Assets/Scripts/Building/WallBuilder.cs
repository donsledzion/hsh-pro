using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallBuilder : DrawWithLines
{
    List<WallSection> _wallSections = new List<WallSection>();
        

    private void OnEnable()
    {
        IsDrawing = true;
    }

    private void OnDisable()
    {
        IsDrawing = false;
    }

    public void AddWallSection()
    {
        Vector2[] allPoints = _drawing2DController.LinePoints;
        int allPointsCount = allPoints.Length;
        if (allPointsCount < 2) return;
        Vector2[] sectionPoints = { allPoints[allPointsCount - 2], allPoints[allPointsCount - 1] };
        WallSection section = new SectionStraight(sectionPoints);

        _wallSections.Add(section);
    }

    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        Wall wall = _drawing2DController.ApplyWallToBuilding();
        _drawing2DController.StoreWall(wall);
        _drawing2DController.ClearCurrentLine();
    }

    protected override void HandleClick()
    {
        AddWallSection();
    }
}
