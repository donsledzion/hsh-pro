using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRModeRescaler : MonoBehaviour
{
    [SerializeField] UnityEvent _onEnableEvents;
    [SerializeField] UnityEvent _onDisableEvents;
    private void OnEnable()
    {
        _onEnableEvents?.Invoke();
    }

    private void OnDisable()
    {
        _onDisableEvents?.Invoke();
    }
}
