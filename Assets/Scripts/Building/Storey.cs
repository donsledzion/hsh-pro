using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
public class Storey
{
    List<Wall> _walls = new List<Wall>();
    string _name;
    float _elevation;
    float _height;


    public Storey()
    {
        _name = "Default floor";
        _elevation = 0f;
        _height = 3.2f;
    }

    public Storey(string storeyName, float elevation = 0f, float height = 3.2f)
    {
        _name = storeyName;
        _elevation = elevation;
        _height = height;
    }

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

    public Wall AddNewWall()
    {
        foreach(Wall wall in _walls)
            if (wall.WallSections.Length < 1) return wall;
        Wall newWall = new Wall();
        _walls.Add(newWall);
        return newWall;
    }

    public override string ToString()
    {
        string basic = "Storey name: " + _name + ", Elevation: " + _elevation + "[m] , Height: " + _height + "[m].";
        string walls = "Walls: " + _walls.Count + "\n";
        if(_walls.Count > 0)
        {
            walls += "\n";
            foreach (Wall wall in _walls)
                walls += wall.ToString();
        }
        return basic + walls;
    }
}
