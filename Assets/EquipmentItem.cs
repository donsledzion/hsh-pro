using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField] Vector3 _thumbScale = new Vector3(1f, 1f, 1f);
    [SerializeField] LayerMask _targetLayer;

    public LayerMask TargetLayer { get { return _targetLayer; } }

    public Vector3 ThumbScale
    { 
        get
        {
            return _thumbScale;
        } 
    }
}
