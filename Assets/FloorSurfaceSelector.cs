using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSurfaceSelector : SurfaceSelector
{
    Vector2 floorTiling = new Vector2(100f,100f);
    Vector2 originalTilling = new Vector2();
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

    protected override void SetTiling(Vector2 tilling = new Vector2())
    {
        _selection.GetComponent<FloorPlane>().SetTilling(FloorTiling);
    }

    protected override void TryMaterial()
    {
        Material originalMaterial = _selection.GetComponent<MeshRenderer>().material;
        originalTilling = 100f * originalMaterial.mainTextureScale;
        _originalMaterial = originalMaterial;
        _selection.GetComponent<MeshRenderer>().material = _selectionMaterial;


        SetTiling();
    }

    protected override void RestoreMaterial()
    {
        Debug.Log("Restoring material");
        FloorPlane plane = _selection.GetComponent<FloorPlane>();
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
        plane.SetTilling(originalTilling);
        
    }
}
