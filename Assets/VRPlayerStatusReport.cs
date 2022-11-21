using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerStatusReport : MonoBehaviour
{
    public static VRPlayerStatusReport ins { get; private set; }

    [SerializeField] Transform _vrPlayerTransform;

    public bool IsActive => _vrPlayerTransform.gameObject.activeSelf;

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
