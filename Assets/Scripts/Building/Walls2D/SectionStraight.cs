using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    [XmlType(TypeName = "section-straight")]
    public class SectionStraight : WallSection
    {
        /*public SectionStraight()
        {
            _startPoint = new BasePoint();
            _endPoint = new BasePoint();
            _paintingSetup = new StraightSectionPaintingSetup();
            _orderInWall = -1;
        }*/

        public SectionStraight(Vector2[] _points)
        {
            _startPoint = new BasePoint(_points[0],this);
            _endPoint = new BasePoint(_points[1],this);
            _paintingSetup = new StraightSectionPaintingSetup();
            _orderInWall = -1;
        }
        public SectionStraight(Vector2 startPoint, Vector2 endPoint)
        {
            _startPoint = new BasePoint(startPoint,this);
            _endPoint = new BasePoint(endPoint, this);
            _paintingSetup = new StraightSectionPaintingSetup();
            _orderInWall = -1;
        }
        public SectionStraight(SectionStraight sectionStraight)
        {
            _startPoint = new BasePoint(sectionStraight.StartPoint.Position, this);
            _endPoint = new BasePoint(sectionStraight.EndPoint.Position, this);
            _paintingSetup = sectionStraight.PaintingSetup;
            _orderInWall = -1;
        }

        public override WallSection Clone()
        {
            return new SectionStraight(this);
        }
    }
}