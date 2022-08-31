using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class Windowjamb : WallSection
    {

        float _width;
        float _height;

        public Windowjamb()
        {
            _width = 80f;
            _height = 200f;
            _startPoint = new BasePoint();
            _endPoint = new BasePoint();
        }

        public override WallSection Clone()
        {
            return new Windowjamb(_width,_height,_startPoint.Position,_endPoint.Position);
        }

        public Windowjamb(float width, float height, Vector2 startPoint, Vector2 endPoint)
        {
            _width = width;
            _height = height;
            _startPoint.Position = startPoint;
            _endPoint.Position = endPoint;
        }

        public float Width { get { return _width; } }
        public float Height { get { return _height; } }

        public void SetAnchors(WallSection wallSection, Vector2 position)
        {
            Vector2 wallVersor = (wallSection.EndPoint.Position - wallSection.StartPoint.Position).normalized;
            StartPoint.Position = position - wallVersor * _height / 4f;
            EndPoint.Position = position + wallVersor * _height / 4f;
        }
    }
}