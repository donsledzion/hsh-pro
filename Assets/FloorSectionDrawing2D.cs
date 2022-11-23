using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using TMPro;

public class FloorSectionDrawing2D : MonoBehaviour
{
    [SerializeField] Transform TMPAreaTransform;
    [SerializeField] TextMeshProUGUI _areaTMP;
    [SerializeField] Transform LineRendererTransform;
    [SerializeField] UILineRenderer _lineRenderer;
    [SerializeField] Vector2[] _points;

    public void DrawOnCanvas(Vector2[] points )
    {
        _points = points;
        _areaTMP.transform.localPosition = PolygonHelper.FindCentroid(new List<Vector2>(_points));
        _areaTMP.text = (PolygonHelper.FindPolygonArea(points)/10000).ToString("0.00") + " m\xB2";
        _lineRenderer.Points = DrawWithLines.EnsureLineIsClosed(points, Drawing2DController.ins.FirstToLastTollerance); ;
        _lineRenderer.LineThickness += .1f;
        _lineRenderer.LineThickness -= .1f;
    }

    public void DrawOnCanvas(FloorSection2D floor)
    {
        DrawOnCanvas(floor.Points);
    }

    public void DrawOnCanvas()
    {
        DrawOnCanvas(_points);
    }

    public void SetVisibility(bool isVisible)
    {
        TMPAreaTransform.gameObject.SetActive(isVisible);
        LineRendererTransform.gameObject.SetActive(isVisible);

    }
}
