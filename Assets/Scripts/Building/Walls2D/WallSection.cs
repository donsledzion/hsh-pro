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
    public abstract class WallSection/* : MonoBehaviour*/
    {
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

        
        public static XElement Serialize(WallSection wallSection)
        {
            return new XElement("wall-section", wallSection);
        }
    }


    
}