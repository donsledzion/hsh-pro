using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Walls2D;

public class SingleMeasure : MonoBehaviour
{
    UILineRenderer _uILineRenderer;
    void Awake()
    {
        _uILineRenderer = gameObject.GetComponent<UILineRenderer>();
    }

    public void Draw(WallSection section, float lineOffset)
    {

        float angle = MathHelpers.VectorAzimuthRad(section.EndPoint.Position - section.StartPoint.Position);
        Debug.Log("Angle: " + angle);
        Vector2 offset = new Vector2(-lineOffset * Mathf.Cos(angle + Mathf.PI/2), lineOffset * Mathf.Sin(angle + Mathf.PI / 2));
        Vector2[] points = { section.StartPoint.Position + offset, section.EndPoint.Position + offset };
        _uILineRenderer.Points = points;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
    }
}
