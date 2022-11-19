using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SurfaceSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    [SerializeField] protected Material _selectionMaterial;
    [SerializeField] protected Material _originalMaterial;
    string _materialName;


    public Material SelectionMaterial
    { 
        get { return _selectionMaterial; }
        set {_selectionMaterial = value; }
    }

    public String MaterialName
    {
        get { return _materialName; }
        set { _materialName = value; }
    }




    protected virtual void Update()
    {
        if (_selection != null)
        {
            RestoreMaterial();            
            SetTiling();
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            _selection = hit.transform;
            TryMaterial();            
        }

        if (_selection != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ApplyMaterial();
            }
        }
    }

    protected abstract void TryMaterial();
    protected abstract void RestoreMaterial();
    protected abstract void ApplyMaterial();
    protected abstract void SetTiling(Vector2 tilling = new Vector2());

}
