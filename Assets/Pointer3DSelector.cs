using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class Pointer3DSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] Camera _pcCamera;
    [SerializeField] string _bundleItemName="";
    public GameObject ItemPrefab
    {
        get { return _itemPrefab; } 
        set { _itemPrefab = value;}
    }

    public string BundleItemName
    {
        get { return _bundleItemName; }
        set { _bundleItemName = value;}
    }




    protected virtual void Update()
    {
        if (_selection != null)
        {
            _selection = null;
        }
        var ray = _pcCamera.ScreenPointToRay(Input.mousePosition);
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
        _selection.GetComponent<BoxCollider>().enabled = false;

        WallSectionAlt section3D = _selection.GetComponentInParent<WallSectionAlt>();

        GameObject doorInstance = Instantiate(_itemPrefab, section3D.transform);
        Vector3 prefabSize = doorInstance.GetComponent<BoxCollider>().size;
        Vector3 jambSize = _selection.GetComponent<BoxCollider>().size;
        doorInstance.GetComponent<BoxCollider>().enabled = false;

        Vector3 scaleFactor = _selection.GetComponentInParent<PhantomScaler>().transform.localScale;
        //Debug.Log("Scale factor.z: " + scaleFactor.z + " | jambSize.z: " + jambSize.z + " | prefabSize.z: " + prefabSize.z);

        doorInstance.transform.localScale = new Vector3(
                scaleFactor.x * jambSize.x / prefabSize.x,
                scaleFactor.y * jambSize.y / prefabSize.y,
                10 * jambSize.z / prefabSize.z);

        doorInstance.transform.SetParent(_selection);
        doorInstance.transform.localPosition = Vector3.zero;
        doorInstance.transform.localRotation = Quaternion.identity;
        Jamb jamb = section3D.Section as Jamb;
        if (jamb != null)
        { 
            Debug.Log("Found Jamb");
            jamb.AssignJoinery(BundleItemName);
        }
        else
        {
            Debug.Log("Couldn't load Jamb!");
        }
    }
}
