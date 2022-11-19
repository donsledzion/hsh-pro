using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPrefabController : MonoBehaviour
{
    public static CurrentPrefabController ins { get; private set; }

    [SerializeField] Pointer3DSelector _door3DSelector;
    [SerializeField] Pointer3DSelector _window3DSelector;

    public Pointer3DSelector Door3DSelector { get { return _door3DSelector; } }
    public Pointer3DSelector Window3DSelector { get { return _window3DSelector; } }


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
