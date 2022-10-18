using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class DrawCeiling : DrawWithLines
{
    [SerializeField] GameObject ceilingPrefab;
    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        GameObject ceiling = Instantiate(ceilingPrefab,_drawing2DController.CurrentStorey.gameObject.transform);
        UILineRenderer lineRenderer = ceiling.GetComponent<UILineRenderer>();
        lineRenderer.Points = _drawing2DController.LinePoints;
        lineRenderer.LineThickness += .1f;
        lineRenderer.LineThickness -= .1f;        
        GameManager.ins.AddCeilingToCurrentStorey(FilterPoints(lineRenderer.Points));
        _drawing2DController.ClearCurrentLine();
        Debug.Log("Finishing drawing ceiling");
    }

    protected override void HandleClick()
    {
        Debug.Log("Drawing floor click");
    }
}
