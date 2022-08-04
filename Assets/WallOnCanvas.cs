using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class WallOnCanvas : MonoBehaviour
{
    UILineRenderer _uILineRenderer;

    private void Awake()
    {
        _uILineRenderer = gameObject.GetComponent<UILineRenderer>();
    }

    public void DrawOnCanvas(Vector2[] points)
    {
        _uILineRenderer.Points = new Vector2[points.Length];
        _uILineRenderer.Points = points;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
    }

    public void SetThickness(float thickness)
    {
        _uILineRenderer.LineThickness = thickness;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
    }
}
