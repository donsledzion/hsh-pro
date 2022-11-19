using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSection : MonoBehaviour
{
    [SerializeField] List<CeilingPlane> _ceilingPlanes = new List<CeilingPlane>();
    [SerializeField] List<CeilingBand> _ceilingBands = new List<CeilingBand>();
    public List<CeilingPlane> CeilingPlanes { get { return _ceilingPlanes; } }
    public List<CeilingBand> CeilingBands { get { return _ceilingBands; } }
}