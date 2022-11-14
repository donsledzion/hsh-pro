using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

[XmlType("floor-section")]
[Serializable]
public class FloorSection2D
{
    int _order;
    float _topLevel;
    Vector2[] _points;

    public int Order { get { return _order; } set { _order = value; } }
    public float TopLevel { get { return _topLevel; } set { _topLevel = value; } }
    public Vector2[] Points { get { return _points; } set { _points = value; } }

    public FloorSection2D()
    {
        _order = 0;
        _topLevel = 0;
        _points = new Vector2[0];
    }


    public FloorSection2D(int order = 0 , float topLevel = 0)
    {
        _order = order;
        _topLevel = topLevel;
        _points = new Vector2[0];
    }

    public FloorSection2D(int order, float topLevel, Vector2[] points)
    {
        _order = order;
        _topLevel = topLevel;
        _points = points;
    }

    public XmlSerializer SerializeToXML()
    {
        var XML = new XmlSerializer(typeof(FloorSection2D));

        return XML;
    }

    public static XElement Serialize(FloorSection2D floorSection)
    {
        return new XElement("floor-section", floorSection);
    }
}
