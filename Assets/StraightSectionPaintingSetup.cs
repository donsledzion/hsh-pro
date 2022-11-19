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
        string _aHeadPaintingA;
        string _aHeadPaintingB;
        string _bHeadPaintingA;
        string _bHeadPaintingB;



        public StraightSectionPaintingSetup()
        {
            _aFacePainting = "";
            _bFacePainting = "";
            _aHeadPaintingA = "";
            _aHeadPaintingB = "";
            _bHeadPaintingA = "";
            _bHeadPaintingB = "";
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

        public string AHeadPaintingA
        {
            get { return _aHeadPaintingA; }
            set {_aHeadPaintingA = value; }
        }
        public string AHeadPaintingB
        {
            get { return _aHeadPaintingB; }
            set {_aHeadPaintingB = value; }
        }

        public string BHeadPaintingA
        {
            get { return _bHeadPaintingA; }
            set {_bHeadPaintingA = value; }
        }

        public string BHeadPaintingB
        {
            get { return _bHeadPaintingB; }
            set {_bHeadPaintingB = value; }
        }

        public override void AssignMaterial(string planeName, string materialName)
        {
            switch(planeName)
            {
                case "PlaneHeadStartLeft":
                    _aHeadPaintingA = materialName;
                    break;
                case "PlaneHeadStartRight":
                    _aHeadPaintingB = materialName;
                    break;
                case "PlaneHeadEndLeft":
                    _bHeadPaintingA = materialName;
                    break;
                case "PlaneHeadEndRight":
                    _bHeadPaintingB = materialName;
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
