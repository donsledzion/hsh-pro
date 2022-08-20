using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    [SerializeField] bool shouldRotate = false;

    void Update()
    {
        if(shouldRotate)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
