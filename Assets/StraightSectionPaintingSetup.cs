using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    public class StraightSectionPaintingSetup : WallSectionPaintingSetup
    {
        string _aFacePainting;
        string _bFacePainting;
        string _aHeadPaintginA;
        string _aHeadPaintginB;
        string _bHeadPaintginA;
        string _bHeadPaintginB;



        public StraightSectionPaintingSetup()
        {
            _aFacePainting = "";
            _bFacePainting = "";
            _aHeadPaintginA = "";
            _aHeadPaintginB = "";
            _bHeadPaintginA = "";
            _bHeadPaintginB = "";
        }

        public string AFacePainting
        {
            get { return _aFacePainting; }
            set {_aFacePainting = value; }
        }

        public string BFacePainting
        {
            get { return _bFacePainting; }
            set {_bFacePainting = value; }
        }

        public string AHeadPaintginA
        {
            get { return _aHeadPaintginA; }
            set {_aHeadPaintginA = value; }
        }
        public string AHeadPaintginB
        {
            get { return _aHeadPaintginB; }
            set {_aHeadPaintginB = value; }
        }

        public string BHeadPaintginA
        {
            get { return _bHeadPaintginA; }
            set {_bHeadPaintginA = value; }
        }

        public string BHeadPaintingB
        {
            get { return _bHeadPaintginB; }
            set {_bHeadPaintginB = value; }
        }

        public override void AssignMaterial(string planeName, string materialName)
        {
            switch(planeName)
            {
                case "PlaneHeadStartLeft":
                    _aHeadPaintginA = materialName;
                    break;
                case "PlaneHeadStartRight":
                    _aHeadPaintginB = materialName;
                    break;
                case "PlaneHeadEndLeft":
                    _bHeadPaintginA = materialName;
                    break;
                case "PlaneHeadEndRight":
                    _bHeadPaintginB = materialName;
                    break;
                case "PlaneFaceLeft":
                    _aFacePainting = materialName;
                    break;
                case "PlaneFaceRight":
                    _bFacePainting = materialName;
                    break;
                default:
                    Debug.LogError("Can't assign material! Unknown plane name: " + planeName);
                    break;
            }
        }
    }

}
