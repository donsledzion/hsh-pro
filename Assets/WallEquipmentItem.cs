using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEquipmentItem : EquipmentItem
{
    [SerializeField] protected LayerMask _wallLayer;

    bool _isTouchingSnapSurface = false;

    public bool IsTouchingSnapSurface { get { return _isTouchingSnapSurface; } }

    [SerializeField] Collider _snapCollider;

    public Collider SnapCollider { get { return _snapCollider; } }   

    public LayerMask WallLayer {  get { return _wallLayer; } }


    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);

        if (_wallLayer == (_wallLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("Snap collision!");
            _isTouchingSnapSurface = true;
            _snapCollider = other;
        }

            
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (_wallLayer == (_wallLayer | (1 << other.gameObject.layer)))
        {
            _isTouchingSnapSurface = false;
            _snapCollider = null;
        }
    }
}
