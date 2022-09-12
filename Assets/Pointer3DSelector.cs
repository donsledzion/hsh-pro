using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer3DSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject _jjDoorPrefab;

    public GameObject DoorPrefab {
        get
        {
            return _jjDoorPrefab;
        } 
        set
        {
            _jjDoorPrefab = value;
        }
    }

    private void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (_selection != null)
        {
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            _selection = hit.transform;
            Debug.Log("Selection: " + _selection.gameObject.name) ;
        }

        if(_selection != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                FitDoorIntoJamb();
            }
        }
    }

    private void FitDoorIntoJamb()
    {
        _selection.GetComponent<MeshRenderer>().enabled = false;
        GameObject doorInstance = Instantiate(_jjDoorPrefab, _selection.GetComponentInParent<WallSectionDoorjamb>().transform);
        doorInstance.transform.SetParent(_selection);
        doorInstance.transform.localPosition = Vector3.zero;
        doorInstance.transform.localRotation = Quaternion.identity;
    }
}
