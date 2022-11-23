using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using TMPro;

public class FloorSectionDrawing2D : MonoBehaviour
{
    [SerializeField] UILineRenderer _lineRenderer;
    [SerializeField] TextMeshProUGUI _areaTMP;
    [SerializeField] Vector2[] _points;
    
    public void Draw(Vector2[] points )
    {
        _points = points;
        _areaTMP.transform.localPosition = PolygonHelper.FindCentroid(new List<Vector2>(_points));
        _areaTMP.text = (PolygonHelper.FindPolygonArea(points)/10000).ToString("0.00") + " m\xB2";
        _lineRenderer.Points = points;
        _lineRenderer.LineThickness += .1f;
        _lineRenderer.LineThickness -= .1f;
    }

    public void Draw(FloorSection2D floor)
    {
        Draw(floor.Points);
    }

    public void Draw()
    {
        Draw(_points);
    }
}
