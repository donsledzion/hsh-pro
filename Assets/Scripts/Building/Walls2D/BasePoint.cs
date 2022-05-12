using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class BasePoint : MonoBehaviour
    {
        Vector2 _position = new Vector2();

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