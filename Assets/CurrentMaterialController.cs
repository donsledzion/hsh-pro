using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMaterialController : MonoBehaviour
{
    public static CurrentMaterialController ins { get; private set; }

    [SerializeField] SurfaceSelector _surfaceSelector;

    public SurfaceSelector SurfaceSelector { get { return _surfaceSelector; } }

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }
}
