using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomScaler : MonoBehaviour
{
    [SerializeField] protected Transform _rendererTransform;

    public Transform RendererTransform { get { return _rendererTransform; } }

    public PhantomRenderer PhantomRenderer =>  _rendererTransform.GetComponent<PhantomRenderer>();
}
