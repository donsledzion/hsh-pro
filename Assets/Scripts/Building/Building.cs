using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    string _name;
    List<Storey> _storeys = new List<Storey>();
    List<Stairs> _stairs = new List<Stairs>();


    public Building()
    {
        _name = "Default building";
        _storeys.Add(new Storey());
    }
    public Building(string buildingName, string storeyName = "Default floor", float elevation = 0f, float height = 3.2f)
    {
        _name = buildingName;
        _storeys.Add(new Storey(storeyName,elevation,height));
    }

    public override string ToString()
    {
        return _name;
    }
}
