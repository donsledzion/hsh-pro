using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Walls2D;
using TMPro;

public class SingleMeasure : MonoBehaviour
{
    UILineRenderer _uILineRenderer;
    [SerializeField] Transform _labelText;
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
        else if(Mathf.Abs(angle) < 0.01f )
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
        Vector2 offset = new Vector2(-lineOffset * Mathf.Cos(angle + Mathf.PI / 2), -lineOffset * Mathf.Sin(angle + Mathf.PI / 2));

        _labelText.position = section.MidPoint;
        _labelText.gameObject.GetComponent<TextMeshPro>().text = section.Length.ToString();

        Vector2[] points = { (section.StartPoint.Position + offset * factor), (section.EndPoint.Position + offset * factor) };
        _uILineRenderer.Points = points;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
    }
}
