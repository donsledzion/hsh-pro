using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInsertionMode : MonoBehaviour
{
    [SerializeField] GameObject _equipmentPrefab;
    [SerializeField] GameObject _equipmentInstance;

    [SerializeField] LayerMask _layerMask;
    protected Transform _selection;
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
        }
            
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask))
        {
            _selection = _hit.transform;
            Debug.Log("Selection: " + _selection.gameObject.name);
        }

        if(_selection != null)
        {
            if(_equipmentInstance == null)
                _equipmentInstance = Instantiate(_equipmentPrefab,_selection);
        }
        else if(_equipmentInstance != null)
        {
            DisposeOfEquipmentInstance();
        }
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
        }
    }
}
