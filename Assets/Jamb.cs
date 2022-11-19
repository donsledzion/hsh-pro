using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
[Serializable]
public abstract class Jamb : WallSection
{
    protected float _width;
    protected float _height;
    protected string _joineryBundle;
    protected string _joineryName;

    public float Width { get { return _width; } set { _width = value; } }
    public float Height { get { return _height; } set { _height = value; } }

    public string JoineryBundle { get { return _joineryBundle; } set { _joineryBundle = value; } }

    public string JoineryName { get { return _joineryName; } set { _joineryName = value; } }

    public void SetAnchors(WallSection wallSection, Vector2 position, float width)
    {
        Vector2 wallVersor = (wallSection.EndPoint.Position - wallSection.StartPoint.Position).normalized;
        StartPoint.Position = position - wallVersor * width/2f;
        EndPoint.Position = position + wallVersor * width/ 2f;
    }

    public void AssignJoinery(string path, string itemName)
    {
        _joineryBundle = path;
        _joineryName = itemName;
    }

    public void RemoveJoinery()
    {
        AssignJoinery("", "");
    }
}
