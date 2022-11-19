using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    [XmlInclude(typeof(StraightSectionPaintingSetup))]
    public abstract class WallSectionPaintingSetup
    {
        public abstract void AssignMaterial(string planeName, string materialName);
    }

}
