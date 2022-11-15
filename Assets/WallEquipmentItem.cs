using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;

public class WallEquipmentItem : EquipmentItem
{
    [SerializeField] protected LayerMask _wallLayer;
    [SerializeField] protected float _snapDistance = 30;
    bool _isTouchingSnapSurface = false;

    public bool IsTouchingSnapSurface { get { return _isTouchingSnapSurface; } }

    Plane _snapPlane;

    GameObject _duplicate;

    public GameObject Duplicate { get { return _duplicate; } }

    public Plane SnapPlane { get { return _snapPlane; } }

    Collider _hitCollider;

    public bool isDuplicate = false;

    public new bool IsColliding
    {
        get
        {
            if (_duplicate == null)
                return _isColliding;
            return _duplicate.GetComponent<WallEquipmentItem>()._isColliding;
        }
    }

    public Transform MountTransform
    {
        get
        {
            if (_duplicate == null)
                return transform;
            return _duplicate.transform;
        }
    }

    public LayerMask WallLayer { get { return _wallLayer; } }


    private void Update()
    {
        if (isDuplicate) return;
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;        
        if (Physics.Raycast(ray, out hit, 200f, WallLayer))
        {
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, ray.direction * _snapDistance, Color.green);
#endif
            if (_hitCollider != hit.collider)
                GetRidOffDuplicate();

            _hitCollider = hit.collider;
            if (_hitCollider.gameObject.CompareTag("WallSnapSurface"))
            {
                _snapPlane = _hitCollider.GetComponent<WallPlane>().Plane;

                if (_duplicate == null)
                {
                    
                    transform.eulerAngles = new Vector3(transform.eulerAngles.z, hit.collider.transform.eulerAngles.y - (_snapPlane.GetSide(transform.position) ? 180 : 0), transform.eulerAngles.z);
                    _duplicate = Instantiate(this.gameObject, this.transform.parent);
                    _duplicate.GetComponent<WallEquipmentItem>().MarkAsDuplicate();
                    _duplicate.GetComponent<WallEquipmentItem>().enabled = false;
                    RenderMe(false);
                }
                _duplicate.transform.position = hit.point;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, ray.direction * _snapDistance, Color.red);
#endif
            GetRidOffDuplicate();
        }
    }

    void GetRidOffDuplicate()
    {
        RenderMe(true);
        Destroy(_duplicate);
        _duplicate = null;
    }

    private void RenderMe(bool state)
    {
        foreach(MeshRenderer renderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = state;
        }
    }

    public void MarkAsDuplicate()
    {
        isDuplicate = true;
    }

    private void OnDestroy()
    {
        Destroy(Duplicate);
    }
}
