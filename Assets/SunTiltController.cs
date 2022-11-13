using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SunTiltController : MonoBehaviour
{
    [SerializeField] LinearMapping _linearMapping;
    [SerializeField] Transform _sunTransform;

    // Update is called once per frame
    void Update()
    {
        _sunTransform.eulerAngles = new Vector3(90*_linearMapping.value, _sunTransform.eulerAngles.y, _sunTransform.eulerAngles.z);
    }
}
