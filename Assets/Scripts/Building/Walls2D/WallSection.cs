using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Xml.Linq;

namespace Walls2D
{
    [XmlInclude(typeof(SectionStraight))]
    [XmlType("wall-section")]
    [Serializable]
    public abstract class WallSection
    {
        protected Wall _wallReference;
        [XmlAttribute]
        protected BasePoint _startPoint;
        [XmlAttribute]
        protected BasePoint _endPoint;

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

        public float AzimuthRad
        {
            get
            {
                return MathHelpers.VectorAzimuthRad(EndPoint.Position - StartPoint.Position);
            }
        }

        public Vector3 MidPoint
        {
            get
            {
                return StartPoint.Position + (EndPoint.Position - StartPoint.Position) / 2;
            }
        }

        public float Length
        {
            get
            {
                return (EndPoint.Position - StartPoint.Position).magnitude;
            }
        }


        public Wall Wall { get { return _wallReference; } }

        public abstract WallSection Clone();

        public void AssignToWall(Wall wall)
        {
            _wallReference = wall;
        }
        public void SplitSection(Vector2 point)
        {
            if (PointBelongsToSection(point))
                Debug.Log("Point belongs to section!");
        }

        public bool PointBelongsToSection(Vector2 point)
        {
            Vector2 lineFactors = MathHelpers.LineFactors(StartPoint.Position, EndPoint.Position);

            float A = lineFactors.x;
            float B = lineFactors.y;



            return point.y == A*point.x+B;
        }

        public override string ToString()
        {
            return "\nStart: [" + StartPoint.Position.x + " , " + StartPoint.Position.y + " ]\n" +
                "End: [" + EndPoint.Position.x + " , " + EndPoint.Position.y + " ]";
        }

        public static XElement Serialize(WallSection wallSection)
        {
            return new XElement("wall-section", wallSection);
        }
    }


    
}