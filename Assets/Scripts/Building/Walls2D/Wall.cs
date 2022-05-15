using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    public class Wall
    {
        WallSection[] _wallSections;
        WallType _wallType;

        public Wall(){}

        public Wall(List<WallSection> wallSections)
        {
            _wallSections = wallSections.ToArray();
        }

        public WallSection[] WallSections
        {
            get { return _wallSections; }
            set { _wallSections = value; }
        }

        public WallType WallType
        {
            get { return _wallType; }
            set { _wallType = value; }
        }

        public static XElement Serialize(Wall wall)
        {
            return new XElement("wall",
                new XAttribute("type", wall.WallType),
                new XElement("sections"),
                from section in wall.WallSections
                select new XElement("section",
                    new XElement("start", section.StartPoint),
                    new XElement("end", section.EndPoint)
                    )
                );
        }

        public override string ToString()
        {
            string strSections = "Sections: ";

            foreach (WallSection section in _wallSections)
            {
                strSections += section.ToString() + "\n";
            }

            return "Type: " + _wallType + "\n" + strSections;
                
    }
    }

   

    public enum WallType
    {
        LoadBearing,
        Partition
    }
}