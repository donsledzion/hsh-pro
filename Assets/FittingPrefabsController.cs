using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingPrefabsController : MonoBehaviour
{
    [SerializeField] List<DoorPhantomScaler> _doorPhantomScalers = new List<DoorPhantomScaler>();
    [SerializeField] List<WindowPhantomScaler> _windowPhantomScalers = new List<WindowPhantomScaler>();

    private void OnEnable()
    {
        InitializeScalers();
    }

    [ContextMenu("Initialize scalers")]
    public void InitializeScalers()
    {
        _doorPhantomScalers = new List<DoorPhantomScaler>(FindObjectsOfType<DoorPhantomScaler>());
        _windowPhantomScalers = new List<WindowPhantomScaler>(FindObjectsOfType<WindowPhantomScaler>());
        HideAll();
    }

    [ContextMenu("Hide all scalers")]
    private void HideAll()
    {
        SetDoorScalers(false);
        SetWindowScalers(false);
    }

    private void SetDoorScalers(bool active)
    {
        foreach (DoorPhantomScaler scaler in _doorPhantomScalers)
            scaler.gameObject.SetActive(active);
    }
    private void SetWindowScalers(bool active)
    {
        foreach (WindowPhantomScaler scaler in _windowPhantomScalers)
            scaler.gameObject.SetActive(active);
    }

    [ContextMenu("Show door scalers")]
    public void ShowDoorScalers()
    {
        SetDoorScalers(true);
    }

    [ContextMenu("Show window scalers")]
    public void ShowWindowScalers()
    {
        SetWindowScalers(true);
    }
}
