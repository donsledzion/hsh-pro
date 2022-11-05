using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif 


public class EquipmentInsertionMode : MonoBehaviour
{
    [SerializeField] GameObject _equipmentPrefab;
    [SerializeField] GameObject _equipmentInstance;

    [SerializeField] Material _materialGood;
    [SerializeField] Material _materialBad;

    [SerializeField] LayerMask _layerMask;
    [SerializeField] LayerMask _collisionLayerMask;
    protected Transform _selection;
/*
    [SerializeField] Vector3 _equipmentBox;
    [SerializeField] Vector3 _equipmentBoxCenter;*/

    public GameObject EquipmentPrefab
    {
        get { return _equipmentPrefab; }
        set { _equipmentPrefab = value; }
    }

    RaycastHit _hit;
    private void OnEnable()
    {
        AssignPrefabLayerMask();
    }

    void Update()
    {
        if (_equipmentInstance != null)
        {
            if(_hit.point != null)
            _equipmentInstance.transform.position = 
                new Vector3(
                    _hit.point.x,
                    GameManager.ins.Building.CurrentStorey.Elevation ,
                    _hit.point.z);
            if (Input.GetKeyDown(KeyCode.R))
                RotateItem();
            if (Input.GetMouseButtonDown(0))
                InsertItem();
            if(_equipmentInstance.GetComponent<EquipmentItem>().IsColliding)
            {                
                RedrawMaterials(_equipmentInstance.transform, _materialBad);
            }
            else
            {
                RedrawMaterials(_equipmentInstance.transform, _materialGood);
            }
        }
            
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask))
        {
            _selection = _hit.transform;
            //Debug.Log("Selection: " + _selection.gameObject.name);
        }

        if(_selection != null)
        {
            if(_equipmentInstance == null)
            {
                _equipmentInstance = Instantiate(_equipmentPrefab,_selection);
                /*_equipmentBox = _equipmentInstance.GetComponent<BoxCollider>().bounds.size;
                _equipmentBoxCenter = _equipmentInstance.GetComponent<BoxCollider>().bounds.center;
                _equipmentInstance.transform.position = new Vector3(_equipmentInstance.transform.position.x, _equipmentInstance.transform.position.y+_equipmentBox.y, _equipmentInstance.transform.position.z);*/
            }
        }
        else if(_equipmentInstance != null)
        {
            DisposeOfEquipmentInstance();
        }
    }

    private void InsertItem()
    {
        if(!_equipmentInstance.GetComponent<EquipmentItem>().IsColliding)
        {
            GameObject insertedItem = Instantiate(_equipmentPrefab, _selection);
            insertedItem.transform.position = _equipmentInstance.transform.position;
            insertedItem.transform.rotation = _equipmentInstance.transform.rotation;
            //insertedItem.transform.Translate(Vector3.up * (_equipmentBox.y/2 - _equipmentBoxCenter.y));
        }
            
    }

    private void RotateItem()
    {
        _equipmentInstance.transform.Rotate(Vector3.up, 90);
    }

    void DisposeOfEquipmentInstance()
    {
        Destroy(_equipmentInstance);
        _layerMask = new LayerMask();
    }

    [ContextMenu("Assign Prefab Layer Mask")]
    void AssignPrefabLayerMask()
    {
        if (_equipmentPrefab != null)
        {
            _layerMask = _equipmentPrefab.GetComponent<EquipmentItem>().TargetLayer;
            _collisionLayerMask = _equipmentPrefab.GetComponent<EquipmentItem>().CollisionLayer;
        }
    }

    void RedrawMaterials(Transform _trasform, Material material)
    {
        foreach(MeshRenderer renderer in _trasform.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = material;
        }
    }
}
