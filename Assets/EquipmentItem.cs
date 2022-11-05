using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class EquipmentItem : MonoBehaviour
{
    [SerializeField] Vector3 _thumbScale = new Vector3(1f, 1f, 1f);
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] LayerMask _collisionLayer;

    bool _isColliding = false;  

    public LayerMask TargetLayer { get { return _targetLayer; } }
    public LayerMask CollisionLayer { get { return _collisionLayer; } }

    public Vector3 ThumbScale { get { return _thumbScale; } }

    public bool IsColliding { get { return _isColliding; }}
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Some collision entered");
        if (_collisionLayer == (_collisionLayer | (1 << other.gameObject.layer)))
        {
            _isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_collisionLayer == (_collisionLayer | (1 << other.gameObject.layer)))
        {
            _isColliding = false;
        }
    }
}
