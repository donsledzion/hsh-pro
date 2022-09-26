using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Material _selectionMaterial;
    [SerializeField] Material _originalMaterial;


    protected virtual void Update()
    {
        if (_selection != null)
        {
            _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
            SetTiling();
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            _selection = hit.transform;
            _originalMaterial = _selection.GetComponent<MeshRenderer>().material;
            _selection.GetComponent<MeshRenderer>().material = _selectionMaterial;
            Debug.Log("Selection: " + _selection.gameObject.name);
        }

        if (_selection != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ApplyMaterial();
            }
        }
    }

    private void ApplyMaterial()
    {
        _originalMaterial = _selectionMaterial;
        SetTiling();
    }

    private void SetTiling()
    {
        Transform scalerTransform = _selection.GetComponentInParent<ScallableSection>().transform;        
        Vector2 textureSize = new Vector2(scalerTransform.localScale.x, scalerTransform.localScale.y);
        Debug.Log("Texture size: " + textureSize);
        _selection.GetComponent<TillingAdjuster>().SetTilling(textureSize);

    }
}
