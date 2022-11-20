using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloorSection : MonoBehaviour
{
    [SerializeField] List<FloorPlane> _floorPlanes = new List<FloorPlane>();
    public List<FloorPlane> FloorPlanes
    {
        get { return _floorPlanes; } 
        set { _floorPlanes = value; }
    }
}
