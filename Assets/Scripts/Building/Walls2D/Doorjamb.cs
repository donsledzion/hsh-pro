using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    public class Doorjamb : Jamb
    {

        public Doorjamb()
        {
            _width = 80f;
            _height = 200f;
            _startPoint = new BasePoint();
            _endPoint = new BasePoint();
        }

        public void SetDoorJambParameters(float height, float width)
        {
            _width = width;
            _height = height;
        }

        public override WallSection Clone()
        {
            return new Doorjamb(_width,_height,_startPoint.Position,_endPoint.Position);
        }

        public Doorjamb(float width, float height, Vector2 startPoint, Vector2 endPoint)
        {
            _width = width;
            _height = height;
            _startPoint.Position = startPoint;
            _endPoint.Position = endPoint;
        }
    }
}