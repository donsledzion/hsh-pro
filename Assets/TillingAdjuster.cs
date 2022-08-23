using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillingAdjuster : MonoBehaviour
{
    [SerializeField] float _verticalTilling = 1f;
    [SerializeField] float _horizontalTilling = 1f;

    [SerializeField] float _baseSize = 300f;

    Material _material;

    private void Start()
    {
        _material = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void SetTilling(Vector2 textureScale)
    {
        _material.mainTextureScale = textureScale;
    }

    /*private void Update()
    {
        _material.mainTextureScale = new Vector2(_verticalTilling,_horizontalTilling);
    }*/
}
