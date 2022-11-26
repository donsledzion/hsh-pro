using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.CodeDom;
using UnityEngine.UIElements;

namespace Walls2D
{
    [XmlInclude(typeof(SectionStraight))]
    [XmlInclude(typeof(Jamb))]
    [XmlInclude(typeof(Doorjamb))]
    [XmlInclude(typeof(Windowjamb))]
    [XmlType("wall-section")]
    [Serializable]
    public abstract class WallSection
    {
        protected Wall _wallReference;
        //[XmlAttribute]
        protected BasePoint _startPoint;
        //[XmlAttribute]
        protected BasePoint _endPoint;
        //[XmlAttribute]
        protected WallSectionPaintingSetup _paintingSetup;

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
        public WallSectionPaintingSetup PaintingSetup
        {
            get { return _paintingSetup; }
            set { _paintingSetup = value; }
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
        public bool SplitSection(Vector2 point)
        {
            Debug.Log("Trying to split section");
            if (PointBelongsToSection(point))
            {
                WallSection sectionClone;
                //WallSection sectionB;
                Debug.Log("Point belongs to section!");
                if(this.GetType() == typeof(SectionStraight))
                {
                    Debug.Log("Splitting section straight");
                    sectionClone = this.Clone();
                    sectionClone.StartPoint.Position = this.StartPoint.Position;
                    sectionClone.EndPoint.Position = point;
                    sectionClone.AssignToWall(this.Wall);
                    this.StartPoint.Position = point;
                    List<WallSection> sections = new List<WallSection>(Wall.WallSections);
                    sections.Add(sectionClone);

                    this.Wall.WallSections = sections.ToArray();
                    return true;
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
            return false;
        }

        public bool PointBelongsToSection(Vector2 point, bool includeEdges=false)
        {
            float offsetTollerance = 2f;

            if(!includeEdges)
            {
                if (((point - StartPoint.Position).magnitude < offsetTollerance)
                    || ((point - EndPoint.Position).magnitude < offsetTollerance))
                {
                    Debug.Log("Point does not belong due to containing edges!");
                    return false;
                }
            }

            Vector2 lineFactors = MathHelpers.LineFactors(StartPoint.Position, EndPoint.Position);
            
            float A = lineFactors.x;
            float B = lineFactors.y;

            float equasionLeft = A * point.x + B;
            float equasionRight = point.y;
            float offset = equasionLeft - equasionRight;
            Debug.Log(" | Distance: " + Mathf.Abs(offset));

            bool belongsToSection = Mathf.Abs(offset) < offsetTollerance;

            if (belongsToSection == true)
                Debug.Log("Point belongs to section!");
            else
                Debug.Log("Point DOES NOT belong to section!");

            return belongsToSection;
        }

        public bool PointLaysWithinSection(Vector2 point)
        {
            float deltaXC = point.x - StartPoint.Position.x;
            float deltaYC = point.y - StartPoint.Position.y;

            float deltaXL = EndPoint.Position.x - StartPoint.Position.x;
            float deltaYL = EndPoint.Position.y - StartPoint.Position.y;

            //if (Mathf.Abs(deltaXC * deltaYL - deltaYC * deltaXL) > 0.05f) return false;

            if (Mathf.Abs(deltaXL) >= Mathf.Abs(deltaYL))
                return deltaXL > 0 ?
                    StartPoint.Position.x <= point.x && point.x <= EndPoint.Position.x :
                    EndPoint.Position.x <= point.x && point.x <= StartPoint.Position.x;
            else
                return deltaYL > 0 ?
                    StartPoint.Position.y <= point.y && point.y <= EndPoint.Position.y :
                    EndPoint.Position.y <= point.y && point.y <= StartPoint.Position.y;
        }

        public bool PointAwayFromEdges(Vector2 point, float minDistance = 10f)
        {
            return (((point - StartPoint.Position).magnitude > minDistance) && ((point - EndPoint.Position).magnitude > minDistance));
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