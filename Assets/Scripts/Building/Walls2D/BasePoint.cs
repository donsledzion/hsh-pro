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


        public BasePoint()
        {
            _position = new Vector2();
        }

        public BasePoint(Vector2 position)
        {
            _position = position;
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        List<WallSection> _connectedSections = new List<WallSection>();

        public List<WallSection> ConnectedSections
        {
            get { return _connectedSections; }
        }
    }
}