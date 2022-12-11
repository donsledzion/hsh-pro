using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SurfaceSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    /*[SerializeField] */protected Material _selectionMaterial;
    /*[SerializeField] */protected Material _originalMaterial;
    /*[SerializeField] */protected Vector2 _originalTilling = Vector2.one * -1f;
    string _materialName;


    Vector2 _surfaceTilling;
    public Vector2 SurfaceTilling
    {
        get { return _surfaceTilling; }
        set { _surfaceTilling = value; }
    }


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
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (_selection != null)
        {
            RestoreMaterial();
            Debug.Log("<color=red>SurfaceSelector->Update(): SurfaceTilling: " + SurfaceTilling);
            /*if(SurfaceTilling != Vector2.zero)            
                SetTiling(SurfaceTilling);*/
            _selection = null;
        }
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
    protected abstract void SetTiling(Vector2 tilling);

}
