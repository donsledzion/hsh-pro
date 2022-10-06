using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.CodeDom;

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
            //Debug.Log("WallSectionStartPoint: " + StartPoint.Position + " | point: " + point);
            //Debug.Log("Trying to split points...");
            if (PointBelongsToSection(point))
            {
                WallSection sectionA;
                WallSection sectionB;
                Debug.Log("Point belongs to section!");
                if(this.GetType() == typeof(SectionStraight))
                {
                    Debug.Log("Splitting section straight");
                    sectionA = new SectionStraight();
                    sectionB = new SectionStraight();
                }
                else
                {
                    Debug.Log("Sorry, but  splitting section of type " + this.GetType().ToString() + " is not allowed.");
                }                    
            }
            else
            {
                Debug.Log("Dude: point does not belong to this section!");
            }                
        }

        public bool PointBelongsToSection(Vector2 point, bool includeEdges=false)
        {
            if(!includeEdges)
            {
                if (((point - StartPoint.Position).magnitude < 0.01f) || ((point - EndPoint.Position).magnitude < 0.01f))
                    return false;
            }

            Vector2 lineFactors = MathHelpers.LineFactors(StartPoint.Position, EndPoint.Position);
            
            float A = lineFactors.x;
            float B = lineFactors.y;

            float equasionLeft = A * point.x + B;
            float equasionRight = point.y;
            float offset = equasionLeft - equasionRight;
            //Debug.Log("Left: " + equasionLeft +  " | Right: " + equasionRight+  " | Distance: " + Mathf.Abs(offset));

            return Mathf.Abs(offset) < 0.01f;
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