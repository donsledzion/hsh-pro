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
        }

        public SectionStraight(Vector2[] _points)
        {
            _startPoint = new BasePoint(_points[0]);
            _endPoint = new BasePoint(_points[1]);
        }
    }
}