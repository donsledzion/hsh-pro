using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Serialization;
using UnityEngine;
using Walls2D;

public class Building
{
    string _name;
    List<Storey> _storeys = new List<Storey>();
    List<Stairs> _stairs = new List<Stairs>();
    Vector2 _sheetSize = new Vector2();
    [XmlIgnore]
    public Storey CurrentStorey { get; private set; }
    public string Name { get { return _name; } set { _name = value; } } 
    public Vector2 SheetSize { get { return _sheetSize; } set { _sheetSize = value; } }
    public List<Storey> Storeys { get { return _storeys; } }

    public Building()
    {
        _name = "Nowy domek";
        _sheetSize = new Vector2(1200f, 950f);
    }
    public Building(string buildingName, string storeyName = "Parter", float elevation = 0f, float height = 320f)
    {
        _name = buildingName;
        _storeys.Add(new Storey(0,storeyName,elevation,height));
        _sheetSize = new Vector2(1200f, 950f);
        SetCurrentStorey(_storeys[0]);
    }

    public Vector2[] Points
    {
        get
        {
            List<Vector2> points = new List<Vector2>();
            foreach(Storey storey in Storeys)
            {
                points.AddRange(storey.WallSectionPoints);
            }
            return points.ToArray();
        }
    }

    /// <summary>
    /// Assigns new sheet size to Building object.
    /// Returns true if there is no validation
    /// problems for new size
    /// </summary>
    /// <param name="newSize"></param>
    /// <returns></returns>
    public bool UpdateSheetSize(Vector2 newSize)
    {

        _sheetSize = newSize;
        //add validation (new size can't be smaller then 
        //building's bounds
        return true;
    }

    public void SetCurrentStorey(Storey storey)
    {
        if (_storeys.Contains(storey))
            CurrentStorey = storey;
        else
            Debug.Log("Sorry, seems like this storey does not belong to this building.");
    }

    public void RemoveWall(Wall wall)
    {

    }


    public void AddStorey(string storeyName, float elevation, float height)
    {
        _storeys.Add(new Storey((uint)(_storeys.Count+1), storeyName, elevation, height));
    }

    public bool RemoveEquipmentItem(string guid)
    {
        foreach(Storey storey in _storeys)
        {
            if (storey.RemoveEquipment(guid))
                return true;
        }

        return false;
    }
        
    public void AddStoreySimple(bool seAsCurrent = true)
    {
        Debug.Log("Adding simple storey");
        string name = "Piêtro_" + (_storeys.Count);
        float elevation = _storeys[_storeys.Count - 1].Elevation + _storeys[_storeys.Count - 1].Height;
        float height = _storeys[_storeys.Count - 1].Height;
        AddStorey(name,elevation,height);
        if (seAsCurrent)
            CurrentStorey = _storeys[_storeys.Count - 1];
    }

    public override string ToString()
    {
        string basics = "Building Name: " + _name + ". Storeys: " + _storeys.Count + ": ";
        string storeys = "";
        foreach(Storey storey in _storeys)
        {
            storeys += storey.ToString() + "\n";
        }
        basics += storeys;
        basics += "\nStairs: " + _stairs.Count;
        string stairs = "";
        foreach (Stairs stair in _stairs)
            stairs += stair.ToString() + "\n";
        return basics + stairs ;
    }

    public void RegenerateSectionsReferences()
    {
        foreach (Storey storey in Storeys)
        {
            foreach (Wall wall in storey.Walls)
            {
                foreach (WallSection section in wall.WallSections)
                    section.AssignToWall(wall);
            }

        }
    }
    public void SerializeToXML(string path)
    {
        Vector2 newSize = ReferenceController.ins.WhiteboardBackgroundInfo.MyRect.rect.size;
        UpdateSheetSize(newSize);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            var XML = new XmlSerializer(typeof(Building));

            XML.Serialize(stream, this);
        }
    }

    public static Building DeserializeFromXML(string path)
    {
        Building building = null;            

        XmlSerializer serializer = new XmlSerializer(typeof(Building));

        StreamReader reader = new StreamReader(path);
        building = (Building)serializer.Deserialize(reader);
        Debug.Log(building.ToString());
        building.SetCurrentStorey(building.Storeys[0]);
        return building;
    }
}
