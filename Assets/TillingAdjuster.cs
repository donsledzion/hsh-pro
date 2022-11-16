using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillingAdjuster : MonoBehaviour
{
    [SerializeField] float _verticalTilling = 1f;
    [SerializeField] float _horizontalTilling = 1f;

    [SerializeField] float _baseSize = 300f;

    MeshRenderer _renderer;

    private void OnEnable()
    {
        _renderer = gameObject.GetComponent<MeshRenderer>();
        SetTilling(new Vector2(_horizontalTilling, _verticalTilling));
    }

    [ContextMenu("Set Tilling")]
    public void SetTilling()
    {
        SetTilling(new Vector2(_horizontalTilling, _verticalTilling));
    }

    public void SetTilling(Vector2 textureScale)
    {
        _renderer.material.mainTextureScale = textureScale / _baseSize;
    }
}
