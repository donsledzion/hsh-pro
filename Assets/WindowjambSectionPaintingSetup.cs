using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    public class WindowjambSectionPaintingSetup : WallSectionPaintingSetup
    {
        string _aUpperFacing;
        string _bUpperFacing;
        string _aUpperBottom;
        string _bUpperBottom;

        string _aLowerFacing;
        string _bLowerFacing;
        string _aLowerTop;
        string _bLowerTop;


        public WindowjambSectionPaintingSetup()
        {
            _aUpperFacing = "";
            _bUpperFacing = "";
            _aUpperBottom = "";
            _bUpperBottom = "";

            _aLowerFacing = "";
            _bLowerFacing = "";
            _aLowerTop = "";
            _bLowerTop = "";
        }

        public string AUpperFacing
        {
            get { return _aUpperFacing; }
            set { _aUpperFacing = value; }
        }

        public string BUpperFacing
        {
            set { _bUpperFacing = value; }
            get { return _bUpperFacing; }
        }

        public string AUpperBottom
        {
            get { return _aUpperBottom; }
            set { _aUpperBottom = value; }
        }

        public string BUpperBottom
        {
            get { return _bUpperBottom; }
            set { _bUpperBottom = value; }
        }

        public string ALowerFacing
        {
            get { return _aLowerFacing; }
            set { _aLowerFacing = value; }
        }

        public string BLowerFacing
        {
            get { return _bLowerFacing; }
            set { _bLowerFacing = value; }
        }

        public string ALowerTop
        {
            get { return _aLowerTop; }
            set { _aLowerTop = value; }
        }

        public string BLowerTop
        {
            get { return _bLowerTop; }
            set { _bLowerTop = value; }
        }


        public override void AssignMaterial(string planeName, string materialName)
        {
            switch (planeName)
            {
                case "UpperPlaneFaceLeft":
                    AUpperFacing = materialName;
                    break;
                case "UpperPlaneFaceRight":
                    BUpperFacing = materialName;
                    break;
                case "UpperPlaneBottomLeft":
                    AUpperBottom = materialName;
                    break;
                case "UpperPlaneBottomRight":
                    BUpperBottom = materialName;
                    break;
                case "LowerPlaneFaceLeft":
                    ALowerFacing = materialName;
                    break;
                case "LowerPlaneFaceRight":
                    BLowerFacing = materialName;
                    break;
                case "LowerPlaneTopLeft":
                    ALowerTop = materialName;
                    break;
                case "LowerPlaneTopRight":
                    BLowerTop = materialName;
                    break;
                default:
                    Debug.LogError("Can't assign material! Unknown plane name: " + planeName);
                    break;
            }
        }
    }

}
