using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionReset : MonoBehaviour
{
    Vector3 _startPosition;
    Quaternion _startRotation;
    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public void ResetCamera()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}
