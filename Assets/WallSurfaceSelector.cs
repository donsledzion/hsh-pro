using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSurfaceSelector : SurfaceSelector
{
    
    Vector2 _wallTilling; 
    public Vector2 WallTilling
    {
        get { return _wallTilling; }
        set { _wallTilling = value; }
    }

    protected override void ApplyMaterial()
    {
        _originalMaterial = _selectionMaterial;
        SetTiling(_wallTilling);
        WallSectionAlt wallSection = _selection.GetComponentInParent<WallSectionAlt>();
        if(wallSection != null)
            wallSection.Section.PaintingSetup.AssignMaterial(_selection.gameObject.name, MaterialName);
    }

    protected override void SetTiling(Vector2 tilling/* = new Vector2()*/)
    {
        Debug.Log("<color=red>Tilling is " + tilling + "</color>");
        Transform scalerTransform = _selection.GetComponentInParent<ScallableSection>().transform;
        Vector2 textureSize = new Vector2(scalerTransform.localScale.x, scalerTransform.localScale.y);
        //Debug.Log("Texture size: " + textureSize);
        _selection.GetComponent<TillingAdjuster>().SetTilling(textureSize);
    }

    protected override void TryMaterial()
    {
        _originalMaterial = _selection.GetComponent<MeshRenderer>().material;
        _selection.GetComponent<MeshRenderer>().material = _selectionMaterial;
        SetTiling(_wallTilling);
    }

    protected override void RestoreMaterial()
    {
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
    }
}
