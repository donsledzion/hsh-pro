using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class DrawFloor : DrawWithLines
{
    [SerializeField] GameObject _floorPrefab;


    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        GameObject floor = Instantiate(_floorPrefab, _drawing2DController.CurrentStorey.gameObject.transform);
        UILineRenderer lineRenderer = floor.GetComponent<UILineRenderer>();
        lineRenderer.Points = _drawing2DController.LinePoints;
        lineRenderer.LineThickness += .1f;
        lineRenderer.LineThickness -= .1f;
        GameManager.ins.AddFloorSectionToCurrentStorey(FilterPoints(lineRenderer.Points),1);
        _drawing2DController.ClearCurrentLine();
        Debug.Log("Finishing drawing floor");
    }

    protected override void HandleClick()
    {
        Debug.Log("EMPTY Handle click in DrawFloor script");
    }

}
