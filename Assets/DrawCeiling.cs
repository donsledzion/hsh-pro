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

    Vector2[] FilterPoints(Vector2[] sourcePoints)
    {
        return FilterFirstLastPoint(sourcePoints);
    }

    Vector2[] FilterFirstLastPoint(Vector2[] sourcePoints)
    {
        Vector2[] newPoints = new Vector2[0];
        bool isFiltered = false;
        if(sourcePoints[0] == sourcePoints[sourcePoints.Length-1])
        {
            newPoints = new Vector2[sourcePoints.Length - 1];
            for(int i = 0; i < newPoints.Length; i++)
            {
                newPoints[i] = sourcePoints[i];
            }
            isFiltered = true;
        }
        Debug.Log("NewPoints.Count: " + newPoints.Length);
        return isFiltered ? newPoints : sourcePoints;
    }

    protected override void HandleClick()
    {
        Debug.Log("Drawing floor click");
    }
}
