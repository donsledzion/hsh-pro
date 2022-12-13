using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoineryPhantomsEnabler : MonoBehaviour
{
    [SerializeField] JoineryType _joineryToEnable;
    [SerializeField] FittingPrefabsController _fittingPrefabsController;

    List<PhantomScaler> phantomScalers
    {
        get
        {
            List<PhantomScaler> scalers = new List<PhantomScaler>();
            scalers.AddRange(_fittingPrefabsController.DoorPhantomScalers);
            scalers.AddRange(_fittingPrefabsController.WindowPhantomScalers);
            return scalers;
        }
    }
    void SetAll(bool state)
    {
        
        Debug.Log("Found : " + phantomScalers.Count + " PR to change state!");
        foreach (PhantomScaler phantomScaler in phantomScalers)
        {
            if(phantomScaler.PhantomRenderer.JoineryType == _joineryToEnable)
            { 
                if (state && !phantomScaler.PhantomRenderer.HasJoinery )
                    phantomScaler.PhantomRenderer.Enable();
                else if(!state)
                    phantomScaler.PhantomRenderer.Disable();
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log("Activating all phantom renderers");
        SetAll(true);
    }

    private void OnDisable()
    {
        Debug.Log("Disabling all phantom renderers");
        SetAll(false);
    }
}
