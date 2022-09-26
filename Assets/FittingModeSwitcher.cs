using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingModeSwitcher : MonoBehaviour
{

    [SerializeField] GameObject _fittingDoorTool;
    [SerializeField] GameObject _fittingWindowsTool;
    [SerializeField] GameObject _surfaceSelector;

    public void DoorFittingMode()
    {
        SwitchOffAll();
        _fittingDoorTool.SetActive(true);
    }

    public void WallsPaintingMode()
    {
        SwitchOffAll();
        _surfaceSelector.SetActive(true);
    }

    public void WindowsFittingMode()
    {
        SwitchOffAll();
        _fittingWindowsTool.SetActive(true);
    }

    public void SwitchOffAll()
    {
        _surfaceSelector.SetActive(false);
        _fittingDoorTool.SetActive(false);
        _fittingWindowsTool.SetActive(false);
    }
}
