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
        float factor = 1f;
        float angle = MathHelpers.VectorAzimuthRad(section.EndPoint.Position - section.StartPoint.Position);
        Debug.Log("Angle: " + angle + "" + ((angle == Mathf.PI) ? " = PI " : " !=PI"));
        
        if (angle > 0 && angle < Mathf.PI)
        {
            Debug.Log("Case 1: ");
            factor = -1f;
        }            
        else if (angle < 0 && angle > -Mathf.PI)
        {
            Debug.Log("Case 2: ");
            angle += Mathf.PI;
            factor = -1f;
        }
        else if(/*Mathf.Abs(Mathf.Abs(angle/Mathf.PI) - 1f) < 0.01f ||*/ Mathf.Abs(angle) < 0.01f )
        {
            Debug.Log("Case 3: ");
            angle /= Mathf.PI;
            factor = -1f;
        }
        else if(angle == Mathf.PI)
        {
            Debug.Log("Case 4: ");
            angle = 0f;
            factor = -1f;
        }
        else
        {
            Debug.Log("Else i huj!");
        }
        Vector2 offset = new Vector2(-lineOffset * Mathf.Cos(angle + Mathf.PI / 2), -lineOffset * Mathf.Sin(angle + Mathf.PI / 2));

        Vector2[] points = { (section.StartPoint.Position + offset * factor), (section.EndPoint.Position + offset * factor) };
        _uILineRenderer.Points = points;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
    }
}
