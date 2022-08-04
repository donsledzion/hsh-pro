using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{

    [SerializeField]
    UnityEvent _onStart;

    void Start()
    {
        _onStart?.Invoke();
    }

}
