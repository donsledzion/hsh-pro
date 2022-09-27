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
        Vector3 prefabSize = doorInstance.GetComponent<BoxCollider>().size;
        Vector3 jambSize = _selection.GetComponent<BoxCollider>().size;

        Vector3 scaleFactor = _selection.GetComponentInParent<PhantomScaler>().transform.localScale;
        Debug.Log("Scale factor.z: " + scaleFactor.z + " | jambSize.z: " + jambSize.z + " | prefabSize.z: " + prefabSize.z);

        doorInstance.transform.localScale = new Vector3(
                scaleFactor.x * jambSize.x / prefabSize.x,
                scaleFactor.y * jambSize.y / prefabSize.y,
                10 * jambSize.z / prefabSize.z);

        doorInstance.transform.SetParent(_selection);
        doorInstance.transform.localPosition = Vector3.zero;
        doorInstance.transform.localRotation = Quaternion.identity;

    }
}
