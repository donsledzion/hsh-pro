using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingModeSwitcher : MonoBehaviour
{

    [SerializeField] GameObject _fittingDoorTool;
    [SerializeField] GameObject _fittingWindowsTool;

    public void DoorFittingMode()
    {
        SwitchOffAll();
        _fittingDoorTool.SetActive(true);
    }

    public void WindowsFittingMode()
    {
        SwitchOffAll();
        _fittingWindowsTool.SetActive(true);
    }

    public void SwitchOffAll()
    {
        _fittingDoorTool.SetActive(false);
        _fittingWindowsTool.SetActive(false);
    }
}
