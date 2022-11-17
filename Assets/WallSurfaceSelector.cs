using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSurfaceSelector : SurfaceSelector
{
    protected override void ApplyMaterial()
    {
        _originalMaterial = _selectionMaterial;
        SetTiling();
    }

    protected override void SetTiling()
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
    }

    protected override void RestoreMaterial()
    {
        _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
    }
}
