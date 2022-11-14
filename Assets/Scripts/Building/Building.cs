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

    [XmlIgnore]
    public Storey CurrentStorey { get; private set; }

    public List<Storey> Storeys { get { return _storeys; } }
    public Building()
    {
        _name = "Default building";
        _storeys.Add(new Storey());
        SetCurrentStorey(_storeys[0]);
    }
    public Building(string buildingName, string storeyName = "Default floor", float elevation = 0f, float height = 320f)
    {
        _name = buildingName;
        _storeys.Add(new Storey(0,storeyName,elevation,height));
        SetCurrentStorey(_storeys[0]);
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
        
    public void AddStoreySimple(bool seAsCurrent = true)
    {
        string name = "Storey_" + (_storeys.Count + 1);
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
        
    public void SerializeToXML()
    {
        using (var stream = new FileStream(@"D:\Serialization\YourFile.xml", FileMode.Create))
        {
            var XML = new XmlSerializer(typeof(Building));

            XML.Serialize(stream, this);
        }
        foreach (Storey storey in _storeys)
        {
            //storey.SerializeToXML();

        }

        

        foreach (Stairs stairs in _stairs)
        {
            //stairs.SerlializeToXML();
        }
    }
}
