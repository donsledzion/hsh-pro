using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Walls2D;
public class Storey
{
    List<Wall> _walls = new List<Wall>();
    List<Ceiling> _ceilings = new List<Ceiling>();
    List<FloorSection2D> _floors= new List<FloorSection2D>();
    List<Equipment> _equipment = new List<Equipment>();

    uint _number;
    string _name;
    float _elevation;
    float _height;

    public List<BasePoint> BasePoints
    {
        get
        {
            List<BasePoint> points = new List<BasePoint>();
            foreach (Wall wall in Walls)
            {
                foreach(WallSection section in wall.WallSections)
                {
                    points.Add(section.StartPoint);
                    points.Add(section.EndPoint);
                }
            }
            return points;                                  
        }
    }

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

    public List<FloorSection2D> Floors
    {
        get { return _floors; }
        set { _floors = value; }
    }

    public uint Number
    {
        get { return _number; }
        set { _number = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public float Elevation
    {
        get { return _elevation; }
        set { _elevation = value; }
    }

    public float Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public List<Equipment> Equipment
    {
        get { return _equipment; }
        set { _equipment = value; }
    }

    public Wall AddNewWall()
    {
        foreach(Wall wall in _walls)
            if (wall.WallSections.Length < 1) return wall;
        Wall newWall = new Wall();
        newWall.OrderInStorey = _walls.Count;
        _walls.Add(newWall);
        return newWall;
    }

    public Wall AddNewWall(Wall newWall)
    {
        newWall.OrderInStorey = _walls.Count;
        _walls.Add(newWall);
        return newWall;
    }

    public bool RemoveWall(Wall wall)
    {
        if (_walls.Contains(wall))
        {
            _walls.Remove(wall);
            PutWallsInOrder();
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

    public FloorSection2D AddNewFloorSection(FloorSection2D floorSection)
    {
        _floors.Add(floorSection);
        return floorSection;
    }

    public bool RemoveFloorSection(FloorSection2D floorSection)
    {
        if(_floors.Contains(floorSection))
        {
            _floors.Remove(floorSection);
            return true;
        }
        return false;

    }

    public Equipment AddNewEquipment(string guid, string assetName, string bundleName, Vector3 position, Vector3 rotation)
    {
        Equipment equipment = new Equipment(guid, assetName, bundleName, position, rotation);
        _equipment.Add(equipment);
        return equipment;
    }

    public bool RemoveEquipment(string guid)
    {
        foreach(Equipment equipmentItem in Equipment)
        {
            if(equipmentItem.GUID == guid)
            {
                _equipment.Remove(equipmentItem);
                return true;
            }
        }
        return false;
    }

    public List<WallSection> Sections
    {
        get
        {
            List<WallSection> sections = new List<WallSection>();
            foreach (Wall wall in Walls)
            {
                foreach (WallSection section in wall.WallSections)
                {
                    sections.Add(section);
                }
            }
            return sections;
        }
    }

    internal void PutWallsInOrder()
    {
        int index = 0;
        foreach (Wall wall in Walls)
        {
            wall.OrderInStorey = index;
            index++;
        }
    }

    public List<WallSection> SectionsOfType(Type type)
    {
        List<WallSection> sections = new List<WallSection>();
        foreach(WallSection section in this.Sections)
        {
            if(section.GetType() == type)
            {
                sections.Add(section);
            }
        }
        return sections;
    }

    public Vector2[] WallSectionPoints
    {
        get
        {
            List<Vector2> points = new List<Vector2>();  
            foreach(WallSection section in SectionsOfType(typeof(SectionStraight)))
            {
                if(!points.Contains(section.StartPoint.Position))
                    points.Add(section.StartPoint.Position);
                if (!points.Contains(section.EndPoint.Position))
                    points.Add(section.EndPoint.Position);
            }
            return points.ToArray();
        }
    }

    public List<BasePoint> BasePointsAtPosition(Vector2 position, Type type, float precission = 1f)
    {
        List<BasePoint> basePointsAtPosition = new List<BasePoint>();
        
        foreach(BasePoint basePoint in BasePoints)
        {
            if (basePoint.WallSection.GetType() == type)
            { 
                if((basePoint.Position - position).magnitude <= precission)
                    basePointsAtPosition.Add(basePoint);

            }
        }

        return basePointsAtPosition;
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



    /*public XmlSerializer SerializeToXML()
    {
        var XML = new XmlSerializer(typeof(Storey));

        return XML;
    }*/
}
