using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetTransform : MonoBehaviour
{
    [SerializeField] Transform _targetParentTransform;
    [SerializeField] Transform _sourceParentTransform;

    private void Start()
    {
        _sourceParentTransform = transform.parent;
    }

    void MoveToTransform(Transform _transform)
    {
        gameObject.transform.parent = _transform;
    }

    public void MoveToTarget()
    {
        MoveToTransform(_targetParentTransform);
        transform.localPosition = new Vector3(0, 0, 0);
    }
    public void MoveBack()
    {
        MoveToTransform(_sourceParentTransform);
    }
}
