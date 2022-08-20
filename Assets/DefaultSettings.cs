using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSettings : MonoBehaviour
{
    public static DefaultSettings ins { get; private set; }

    public float LoadBareringWallWidth = 30f;
    public float PartialWallWidth = 10f;
    public float CeilingHeight = 30f;

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
