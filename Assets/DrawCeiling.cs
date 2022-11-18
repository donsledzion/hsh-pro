using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class DrawCeiling : DrawWithLines
{
    [SerializeField] GameObject ceilingPrefab;
    [SerializeField] float _firstToLastPointTollerance = 0.2f;

    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        Vector2[] points = _drawing2DController.LinePoints;
        points = EnsureLineIsClosed(points, _firstToLastPointTollerance);


        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        GameObject ceiling = Instantiate(ceilingPrefab,_drawing2DController.CurrentStorey.gameObject.transform);
        UILineRenderer lineRenderer = ceiling.GetComponent<UILineRenderer>();
        lineRenderer.Points = points;
        lineRenderer.LineThickness += .1f;
        lineRenderer.LineThickness -= .1f;        
        GameManager.ins.AddCeilingToCurrentStorey(FilterPoints(lineRenderer.Points));
        _drawing2DController.ClearCurrentLine();
    }

    protected override void HandleClick()
    {
        Debug.LogWarning("Drawing floor click");
    }
}
