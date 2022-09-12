using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingPrefabsController : MonoBehaviour
{
    [SerializeField] List<DoorPhantomScaler> _doorPhantomScalers = new List<DoorPhantomScaler>();


    

    private void OnEnable()
    {
        _doorPhantomScalers = new List<DoorPhantomScaler>(FindObjectsOfType<DoorPhantomScaler>());
        HideAll();
    }

    private void HideAll()
    {
        SetDoorScalers(false);
    }

    private void SetDoorScalers(bool active)
    {
        foreach (DoorPhantomScaler scaler in _doorPhantomScalers)
            scaler.gameObject.SetActive(active);
    }

    [ContextMenu("Show door scalers")]
    public void ShowDoorScalers()
    {
        SetDoorScalers(true);
    }
}
