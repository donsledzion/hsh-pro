using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SunCirculationController : MonoBehaviour
{
    [SerializeField] CircularDrive _circularDrive;
    [SerializeField] Transform _sunTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _sunTransform.eulerAngles = new Vector3(_sunTransform.eulerAngles.x, _circularDrive.outAngle ,_sunTransform.eulerAngles.z);
    }
}
