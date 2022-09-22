using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer3DSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject _itemPrefab;

    public GameObject ItemPrefab {
        get
        {
            return _itemPrefab;
        } 
        set
        {
            _itemPrefab = value;
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
                FitItemIntoJamb();
            }
        }
    }

    private void FitItemIntoJamb()
    {
        _selection.GetComponent<MeshRenderer>().enabled = false;
        GameObject doorInstance = Instantiate(_itemPrefab, _selection.GetComponentInParent<WallSectionAlt>().transform);
        Vector3 prefabSize = doorInstance.GetComponent<BoxCollider>().bounds.size;
        Vector3 jambSize = _selection.GetComponent<BoxCollider>().bounds.size;

        doorInstance.transform.localScale = new Vector3(jambSize.x / prefabSize.x, jambSize.y / prefabSize.y, jambSize.z / prefabSize.z);
        doorInstance.transform.SetParent(_selection);
        doorInstance.transform.localPosition = Vector3.zero;
        doorInstance.transform.localRotation = Quaternion.identity;

    }
}
