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
        public SectionStraight()
        {
            _startPoint = new BasePoint();
            _endPoint = new BasePoint();
            _paintingSetup = new StraightSectionPaintingSetup();
        }

        public SectionStraight(Vector2[] _points)
        {
            _startPoint = new BasePoint(_points[0]);
            _endPoint = new BasePoint(_points[1]);
            _paintingSetup = new StraightSectionPaintingSetup();
        }
        public SectionStraight(Vector2 startPoint, Vector2 endPoint)
        {
            _startPoint = new BasePoint(startPoint);
            _endPoint = new BasePoint(endPoint);
            _paintingSetup = new StraightSectionPaintingSetup();
        }
        public SectionStraight(SectionStraight sectionStraight)
        {
            _startPoint = new BasePoint(sectionStraight.StartPoint.Position);
            _endPoint = new BasePoint(sectionStraight.EndPoint.Position);
            _paintingSetup = sectionStraight.PaintingSetup;
        }

        public override WallSection Clone()
        {
            return new SectionStraight(this);
        }
    }
}