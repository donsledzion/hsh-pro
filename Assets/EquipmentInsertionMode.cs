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
    bool _inserting = true;
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

    public void SwapPrefab(GameObject prefab)
    {
        _equipmentPrefab = prefab;
        Destroy(_equipmentInstance);
        _equipmentInstance = null;
        _inserting = true;
    }

    void Update()
    {
        if (!_inserting) return;

        _selection = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask))
        {
            _selection = _hit.transform;
            //Debug.Log("Selection: " + _selection.gameObject.name);
        }

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

            if (Input.GetKeyDown(KeyCode.C))
                AbortInsertion();
                  

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

    private void AbortInsertion()
    {
        _inserting = false;
        _selection = null;
        Destroy(_equipmentInstance);
        _equipmentInstance = null;
        gameObject.SetActive(false);
    }

    private void InsertItem()
    {
        if(!_equipmentInstance.GetComponent<EquipmentItem>().IsColliding)
        {
            GameObject insertedItem = Instantiate(_equipmentPrefab, _selection);
            insertedItem.transform.position = _equipmentInstance.transform.position;
            insertedItem.transform.rotation = _equipmentInstance.transform.rotation;
            insertedItem.transform.rotation = _equipmentInstance.transform.rotation;
        }            
    }

    private void RotateItem()
    {
        _equipmentInstance.transform.Rotate(Vector3.up, 90);
    }

    void DisposeOfEquipmentInstance()
    {
        _selection = null;
        Destroy(_equipmentInstance);
        _equipmentInstance = null;
        //_layerMask = new LayerMask();
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
