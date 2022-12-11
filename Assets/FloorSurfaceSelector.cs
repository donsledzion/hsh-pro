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
        FloorPlane plane = _selection.GetComponent<FloorPlane>();
        plane.InjectMaterial(_selectionMaterial);
        plane.Floor.MaterialName = MaterialName;
        /*_selection.GetComponentInParent<WallSectionAlt>().Section.PaintingSetup
            .AssignMaterial(_selection.gameObject.name, MaterialName);*/
        
    }

    protected override void SetTiling(Vector2 tilling = new Vector2())
    {
        _selection.GetComponent<FloorPlane>().SetTilling(SurfaceTilling);
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
        //FloorPlane plane = _selection.GetComponent<FloorPlane>();
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
        //plane.SetTilling(originalTilling);
        SetTiling(SurfaceTilling);
    }
}
