using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
public class Storey : MonoBehaviour
{
    List<Wall> _walls = new List<Wall>();
    float _elevation;
    float _height;

    public List<Wall> Walls
    {
        get { return _walls; }
        set { _walls = value; }
    }

    public float Elevation
    {
        get { return _elevation; }
        set { _elevation = value; }
    }

    public float Height
    {
        get { return _height; }
        set { _elevation = value; }
    }
}
