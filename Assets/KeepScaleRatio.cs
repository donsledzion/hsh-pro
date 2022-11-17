using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScaleRatio : MonoBehaviour
{
    [SerializeField] Transform parentTransform;


    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.8f/parentTransform.localScale.y , transform.localScale.z);
        transform.localPosition = new Vector3();
    }
}
