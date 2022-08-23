using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillingAdjuster : MonoBehaviour
{
    [SerializeField] float _verticalTilling = 1f;
    [SerializeField] float _horizontalTilling = 1f;

    [SerializeField] float _baseSize = 300f;

    [SerializeField] MeshRenderer _renderer;

    private void OnEnable()
    {
        _renderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void SetTilling(Vector2 textureScale)
    {
        _renderer.material.mainTextureScale = textureScale / _baseSize;
    }

    /*private void Update()
    {
        _material.mainTextureScale = new Vector2(_verticalTilling,_horizontalTilling);
    }*/
}
