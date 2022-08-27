using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSection : MonoBehaviour
{
    [SerializeField] List<CeilingPlane> _ceilingPlanes = new List<CeilingPlane>();

    public List<CeilingPlane> CeilingPlanes { get { return _ceilingPlanes; } }
}