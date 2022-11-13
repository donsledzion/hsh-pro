using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public abstract class Jamb : WallSection
{
    protected float _width;
    protected float _height;

    public float Width { get { return _width; } }
    public float Height { get { return _height; } }


    public void SetAnchors(WallSection wallSection, Vector2 position)
    {
        Vector2 wallVersor = (wallSection.EndPoint.Position - wallSection.StartPoint.Position).normalized;
        StartPoint.Position = position - wallVersor * InsertWindow.Instance.WindowWidth/2f;
        EndPoint.Position = position + wallVersor * InsertWindow.Instance.WindowWidth / 2f;
    }
}
