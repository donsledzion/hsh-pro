using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSurfaceSelector : SurfaceSelector
{
    protected override void ApplyMaterial()
    {
        _originalMaterial = _selectionMaterial;
        _originalTilling = _selection.GetComponent<MeshRenderer>().material.mainTextureScale;
        SetTiling(SurfaceTilling);
        WallSectionAlt wallSection = _selection.GetComponentInParent<WallSectionAlt>();
        if(wallSection != null)
            wallSection.Section.PaintingSetup.AssignMaterial(_selection.gameObject.name, MaterialName);
    }

    protected override void SetTiling(Vector2 tilling)
    {
        if(tilling.x <= 0 || tilling.y <= 0)
        {
            Debug.Log("<color=green>Texture as photo-wallpaper!</color>");
        }    
        else
        {
            Debug.Log("<color=red>Tilling is OK: " + tilling + "</color>");
            Transform scalerTransform = _selection.GetComponentInParent<ScallableSection>().transform;
            Vector2 textureSize = new Vector2(scalerTransform.localScale.x, scalerTransform.localScale.y);
            //Debug.Log("Texture size: " + textureSize);
            _selection.GetComponent<TillingAdjuster>().SetTilling(textureSize);
        }
    }

    protected override void TryMaterial()
    {
        if(_originalTilling == Vector2.one*-1f)
        {
            _originalTilling = _selection.GetComponent<MeshRenderer>().material.mainTextureScale;
            Debug.Log("Stored original tilling as: " + _originalTilling);
        }
        _originalMaterial = _selection.GetComponent<MeshRenderer>().material;
        _selection.GetComponent<MeshRenderer>().material = _selectionMaterial;
        SetTiling(SurfaceTilling);
    }

    protected override void RestoreMaterial()
    {
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
        _selection.GetComponent<MeshRenderer>().material.mainTextureScale = _originalTilling;
        _originalTilling = Vector2.one * -1f;
    }
}
