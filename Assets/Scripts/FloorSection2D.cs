using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSection2D
{
    int _order;
    float _topLevel;
    Vector2[] _points;

    public float Order { get { return _order; } }
    public float TopLevel { get { return _topLevel; } }
    public Vector2[] Points { get { return _points; } }

    public FloorSection2D(int order = 0 , float topLevel = 0)
    {
        _order = order;
        _topLevel = topLevel;
        _points = new Vector2[0];
    }
    public FloorSection2D(int order, float topLevel, Vector2[] points)
    {
        _order = order;
        _topLevel = topLevel;
        _points = points;
    }
}
