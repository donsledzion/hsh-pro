using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class Jamb : WallSection
    {

        float _width;
        float _height;

        public Jamb()
        {
            _width = 80f;
            _height = 200f;
        }

        public override WallSection Clone()
        {
            throw new System.NotImplementedException();
        }

        public Jamb(float width, float height, float thickness, Vector2 anchorA, Vector2 anchorB)
        {
            _width = width;
            _height = height;
        }

        public float Width { get { return _width; } }
        public float Height { get { return _height; } }

        public void SetAnchors(WallSection wallSection, Vector2 position)
        {
            Vector2 wallVersor = (wallSection.EndPoint.Position - wallSection.StartPoint.Position).normalized;
            StartPoint.Position = position - wallVersor * _height / 2f;
            EndPoint.Position = position + wallVersor * _height / 2f;
        }
    }
}