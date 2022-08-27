using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
public class Storey
{
    List<Wall> _walls = new List<Wall>();
    List<Ceiling> _ceilings = new List<Ceiling>();
    uint _number;
    string _name;
    float _elevation;
    float _height;


    public Storey()
    {
        _number = 0;
        _name = "Default floor";
        _elevation = 0f;
        _height = 320f;
    }

    public Storey(uint number, string storeyName, float elevation = 0f, float height = 320f)
    {
        _number = number;
        _name = storeyName;
        _elevation = elevation;
        _height = height;
    }

    public List<Wall> Walls
    {
        get { return _walls; }
        set { _walls = value; }
    }

    public List<Ceiling> Ceilings
    {
        get { return _ceilings; }
        set { _ceilings = value; }
    }

    public uint Number
    {
        get { return _number; }
        private set { _number = value; }
    }

    public string Name
    {
        get { return _name; }
        private set { _name = value; }
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

    public Wall AddNewWall(Wall newWall)
    {
        _walls.Add(newWall);
        return newWall;
    }

    public bool RemoveWall(Wall wall)
    {
        if (_walls.Contains(wall))
        {
            _walls.Remove(wall);
            return true;
        }
        return false;            
    }

    public Ceiling AddNewCeiling(Ceiling newCeiling)
    {
        _ceilings.Add(newCeiling);
        return newCeiling;
    }

    public bool RemoveCeiling(Ceiling ceiling)
    {
        if(_ceilings.Contains(ceiling))
        {
            _ceilings.Remove(ceiling);
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        string basic = "Storey (" + _number + ") name: " + _name + ", Elevation: " + _elevation + "[m] , Height: " + _height + "[m].";
        string walls = "Walls: " + _walls.Count + "\n";
        string ceilings = "Ceilings: " + _ceilings.Count + "\n";
        if(_walls.Count > 0)
        {
            walls += "\n";
            foreach (Wall wall in _walls)
                walls += wall.ToString();
            ceilings += "\n";
            foreach (Ceiling ceiling in _ceilings)
                ceilings += ceiling.ToString();
        }
        return basic + walls + ceilings;
    }
}
