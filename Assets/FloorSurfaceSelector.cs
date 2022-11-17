using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSurfaceSelector : SurfaceSelector
{
    Vector2 floorTiling = new Vector2(100f,100f); 

    public Vector2 FloorTiling
    {
        get { return floorTiling; }
        set { floorTiling = value; }
    }

    protected override void ApplyMaterial()
    {
        _originalMaterial = _selectionMaterial;
        _selection.GetComponent<FloorPlane>().InjectMaterial(_selectionMaterial);
    }

    protected override void SetTiling()
    {
        _selection.GetComponent<FloorPlane>().SetTilling(FloorTiling);
    }

    protected override void TryMaterial()
    {
        _originalMaterial = _selection.GetComponent<MeshRenderer>().material;
        _selection.GetComponent<MeshRenderer>().material = _selectionMaterial;
    }

    protected override void RestoreMaterial()
    {
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
    }
}
