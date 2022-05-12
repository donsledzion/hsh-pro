using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public abstract class WallSection : MonoBehaviour
    {
        protected BasePoint _startPoint;
        protected BasePoint _endPoint;
        protected WallType _wallType;

        public BasePoint StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        public BasePoint EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        public WallType WallType
        {
            get { return _wallType; }
            set { _wallType = value; }
        }
    }

    public enum WallType
    {
        LoadBearing,
        Partition
    }
}