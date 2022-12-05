using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoineryPhantomsEnabler : MonoBehaviour
{
    [SerializeField] JoineryType _joineryToEnable;

    void SetAll(bool state)
    {
        PhantomRenderer[] phantomrenderers = ReferenceController.ins.Builder3D.transform.GetComponentsInChildren<PhantomRenderer>();
        Debug.Log("Found : " + phantomrenderers.Length + " PR to change state!");
        foreach (PhantomRenderer phantomRenderer in phantomrenderers)
        {
            if(phantomRenderer.JoineryType == _joineryToEnable)
            { 
                if (state && !phantomRenderer.HasJoinery )
                    phantomRenderer.Enable();
                else if(!state)
                    phantomRenderer.Disable();
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
