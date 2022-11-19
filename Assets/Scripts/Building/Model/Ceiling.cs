using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Walls2D;

[XmlType("ceiling-section")]
[Serializable]
public class Ceiling
{
    float _thickness;
    float _topLevel;
    Vector2[] _points;


    public float Thickness { get { return _thickness; }
        set
        {
            _thickness = value;
        } }
    public float TopLevel { get { return _topLevel; }
        set
        {
            _topLevel = value;
        } }
    public Vector2[] Points { get { return _points; }
        set
        {
            _points = value;
        } }

    public Ceiling()
    {
        _thickness = 30f;
        _topLevel = 0f;

    }

    public Ceiling(float thickness = 30f, float topLevel = 0f)
    {
        _thickness = thickness;
        _topLevel = topLevel;
        _points = new Vector2[0];
    }

    public Ceiling(float thickness, float topLevel, Vector2[] points)
    {
        _thickness = thickness;
        _topLevel = topLevel;
        _points = points;
    }

    public void SetPoints(Vector2[] points)
    {
        if (points.Length < 3)
            Debug.Log("Ceiling need to has at least three points");
        else
            _points = points;
    }

    public override string ToString()
    {
        string points = "\nPoints: \n";
        foreach (Vector2 point in _points)
            points += point.ToString() + ", ";
        return "Ceiling: thickness - " + Thickness + ", level - " + TopLevel + ", " + points;
    }
}
    