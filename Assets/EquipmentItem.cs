using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class EquipmentItem : MonoBehaviour
{
    string _guid;
    [SerializeField] Vector3 _thumbScale = new Vector3(1f, 1f, 1f);
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] LayerMask _collisionLayer;

    protected bool _isColliding = false;

    protected Equipment _equipment;

    public string GUID { get { return _guid; } set { _guid = value; } }
    public LayerMask TargetLayer { get { return _targetLayer; } }
    public LayerMask CollisionLayer { get { return _collisionLayer; } }

    public Vector3 ThumbScale { get { return _thumbScale; } }

    public bool IsColliding { get { return _isColliding; }}

    public Equipment Equipment { get { return _equipment; } set { _equipment = value; } }

    public void UpdatePosition()
    {
        _equipment.Position = transform.position;
        _equipment.Rotation = transform.eulerAngles;
    }

    private void OnTriggerStay(Collider other)
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

    public void SetTargetLayerMask(LayerMask layerMask)
    {
        _targetLayer = layerMask;
    }

    public void SetCollisionLayerMask(LayerMask layerMask)
    {
        _collisionLayer = layerMask;
    }


}
