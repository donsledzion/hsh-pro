using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    public class BasePoint/* : MonoBehaviour*/
    {
        [XmlAttribute]
        Vector2 _position = new Vector2();
        WallSection _wallSection;

        public WallSection WallSection => _wallSection;

        public BasePoint(WallSection section)
        {
            _position = new Vector2();
            _wallSection = section;
        }

        public BasePoint(Vector2 position, WallSection section)
        {
            _position = position;
            _wallSection = section;
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}