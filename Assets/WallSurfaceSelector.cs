using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSurfaceSelector : SurfaceSelector
{
    protected override void ApplyMaterial()
    {
        _originalMaterial = _selectionMaterial;
        SetTiling();
        _selection.GetComponentInParent<WallSectionAlt>().Section.PaintingSetup.AssignMaterial(_selection.gameObject.name, MaterialName);
    }

    protected override void SetTiling(Vector2 tilling = new Vector2())
    {
        Transform scalerTransform = _selection.GetComponentInParent<ScallableSection>().transform;
        Vector2 textureSize = new Vector2(scalerTransform.localScale.x, scalerTransform.localScale.y);
        Debug.Log("Texture size: " + textureSize);
        _selection.GetComponent<TillingAdjuster>().SetTilling(textureSize);
    }

    protected override void TryMaterial()
    {
        _originalMaterial = _selection.GetComponent<MeshRenderer>().material;
        _selection.GetComponent<MeshRenderer>().material = _selectionMaterial;
        SetTiling();
    }

    protected override void RestoreMaterial()
    {
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
    }
}
