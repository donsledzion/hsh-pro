using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class Windowjamb : Jamb
    {
        float _windowsill;

        public float WindowSill { get { return _windowsill; }}
        public Windowjamb()
        {
            _width = 160f;
            _height = 140f;
            _windowsill = 80f;
            _startPoint = new BasePoint();
            _endPoint = new BasePoint();
        }

        public void SetWindowJambParameters(float height, float width, float windowsill)
        {
            _width = width;
            _height = height;
            _windowsill = windowsill;
        }

        public override WallSection Clone()
        {
            return new Windowjamb(_width,_height,_windowsill,_startPoint.Position,_endPoint.Position);
        }

        public Windowjamb(float width, float height, float windowsill, Vector2 startPoint, Vector2 endPoint)
        {
            _width = width;
            _height = height;
            _windowsill = windowsill;
            _startPoint.Position = startPoint;
            _endPoint.Position = endPoint;
        }
    }
}