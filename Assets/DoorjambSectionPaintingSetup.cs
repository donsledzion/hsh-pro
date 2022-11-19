using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Walls2D
{
    [Serializable]
    public class DoorjambSectionPaintingSetup : WallSectionPaintingSetup
    {
        string _aFacing;
        string _bFacing;
        string _aBottom;
        string _bBottom;

        public DoorjambSectionPaintingSetup()
        {
            _aFacing = "";
            _bFacing = "";
            _aBottom = "";
            _bBottom = "";
        }

        public string AFacing
        {
            get { return _aFacing; }
            set { _aFacing = value; }
        }

        public string BFacing
        {
            set { _bFacing = value; }
            get { return _bFacing; }
        }

        public string ABottom
        {
            get { return _aBottom; }
            set { _aBottom = value; }
        }

        public string BBottom
        {
            get { return _bBottom; }
            set { _bBottom = value; }
        }


        public override void AssignMaterial(string planeName, string materialName)
        {
            switch (planeName)
            {
                case "PlaneFaceLeft":
                    AFacing= materialName;
                    break;
                case "PlaneFaceRight":
                    BFacing = materialName;
                    break;
                case "PlaneBottomLeft":
                    ABottom = materialName;
                    break;
                case "PlaneBottomRight":
                    BBottom = materialName;
                    break;
                
                default:
                    Debug.LogError("Can't assign material! Unknown plane name: " + planeName);
                    break;
            }
        }
    }

}
