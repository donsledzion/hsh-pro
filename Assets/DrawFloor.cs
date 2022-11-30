using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class DrawFloor : DrawWithLines
{
    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        Vector2[] points = _drawing2DController.LinePoints;
        if (points == null || points.Length < 3)
        {
            IsDrawing = false;
            _drawing2DController.ClearLiveLine();
            _drawing2DController.ClearCurrentLine();
            return;
        }

        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        FloorSection2D floor = GameManager.ins.AddFloorSectionToCurrentStorey(FilterPoints(points),1); //TODO - implement assigning displaying order!!!
        _drawing2DController.StoreFloor(floor);
        _drawing2DController.ClearCurrentLine();
    }

    protected override void HandleClick()
    {
        Debug.LogWarning("EMPTY Handle click in DrawFloor script");
    }

}
