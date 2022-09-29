using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionSnapClosePoint : Selector2D
{
    Vector2 _snappedPoint;

    protected override void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        _hoveredSection = ClosestSection(mouseOverCanvas);
        if (_hoveredSection != null)
        {
            _snappedPoint = CastedPoint(_hoveredSection, mouseOverCanvas);
            GameManager.ins.SnappedClosePoint = HoverPoint(_snappedPoint, _hoverColor);            
        }
        else
        {
            UnhoverPoint();
            GameManager.ins.SnappedClosePoint = new Vector2(0f, 0f);
        }
    }

    public override WallSection ClosestSection(Vector2 mouseOverCanvas)
    {
        List<LineSection> segments = new List<LineSection>();
        List<Wall> walls = GameManager.ins.Building.CurrentStorey.Walls;
        WallSection closestSection = null;

        if(Drawing2DController.ins.LinePoints.Length > 1)
        {
            walls.Add(new Wall(Drawing2DController.ins.LinePoints));
        }

        float closestDistance = _lineSnapDistance;
        foreach (Wall wall in walls)
        {
            foreach (WallSection section in wall.WallSections)
            {
                LineSection lSection = new LineSection(section);
                segments.Add(lSection);
                float distance = MathHelpers.PointToLineDistance(lSection.Start, lSection.End, mouseOverCanvas);
                bool pointWithinSection = MathHelpers.DoesPointCastsOnLine(lSection.Start, lSection.End, mouseOverCanvas);
                if ((distance < closestDistance) && pointWithinSection)
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
