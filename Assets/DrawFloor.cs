using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class DrawFloor : DrawWithLines
{
    [SerializeField] float _firstToLastPointTollerance = 0.2f;
    [SerializeField] GameObject _floorPrefab;


    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        Vector2[] points = _drawing2DController.LinePoints;
        points = EnsureLineIsClosed(points, _firstToLastPointTollerance);

        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        GameObject floor = Instantiate(_floorPrefab, _drawing2DController.CurrentStorey.gameObject.transform);
        UILineRenderer lineRenderer = floor.GetComponent<UILineRenderer>();
        lineRenderer.Points = points;
        lineRenderer.LineThickness += .1f;
        lineRenderer.LineThickness -= .1f;
        GameManager.ins.AddFloorSectionToCurrentStorey(FilterPoints(lineRenderer.Points),1);
        _drawing2DController.ClearCurrentLine();
    }

    protected override void HandleClick()
    {
        Debug.LogWarning("EMPTY Handle click in DrawFloor script");
    }

}
