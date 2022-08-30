using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class SectionArc : WallSection
    {
        bool _clockwise = true;

        Vector2 _center;

        public override WallSection Clone()
        {
            throw new System.NotImplementedException();
        }
    }
    
}