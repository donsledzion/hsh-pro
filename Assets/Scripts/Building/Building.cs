using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    string _name;
    List<Storey> _storeys = new List<Storey>();
    List<Stairs> _stairs = new List<Stairs>();
    
    public Storey CurrentStorey { get; private set; }


    public Building()
    {
        _name = "Default building";
        _storeys.Add(new Storey());
        SetCurrentStorey(_storeys[0]);
    }
    public Building(string buildingName, string storeyName = "Default floor", float elevation = 0f, float height = 3.2f)
    {
        _name = buildingName;
        _storeys.Add(new Storey(storeyName,elevation,height));
        SetCurrentStorey(_storeys[0]);
    }

    public void SetCurrentStorey(Storey storey)
    {
        if (_storeys.Contains(storey))
            CurrentStorey = storey;
        else
            Debug.Log("Sorry, seems like this storey does not belong to this building.");
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
}
