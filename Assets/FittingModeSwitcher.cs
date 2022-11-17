using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittingModeSwitcher : MonoBehaviour
{

    [SerializeField] public GameObject _fittingDoorTool;
    [SerializeField] public GameObject _fittingWindowsTool;
    [SerializeField] public GameObject _floorSurfaceSelector;
    [SerializeField] public GameObject _wallSurfaceSelector;

    public void DoorFittingMode()
    {
        SwitchOffAll();
        _fittingDoorTool.SetActive(true);
    }

    public void WallsPaintingMode()
    {
        SwitchOffAll();
        _wallSurfaceSelector.SetActive(true);
    }

    public void FloorFinishingMode()
    {
        SwitchOffAll();
        _floorSurfaceSelector.SetActive(true);
    }

    public void WindowsFittingMode()
    {
        SwitchOffAll();
        _fittingWindowsTool.SetActive(true);
    }

    public void SwitchOffAll()
    {
        _wallSurfaceSelector.SetActive(false);
        _floorSurfaceSelector.SetActive(false);
        _fittingDoorTool.SetActive(false);
        _fittingWindowsTool.SetActive(false);
    }
}
