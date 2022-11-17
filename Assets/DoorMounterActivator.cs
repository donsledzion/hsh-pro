using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMounterActivator : MonoBehaviour
{
    [SerializeField] GameObject _drawTool;

    private void OnEnable()
    {
        _drawTool.SetActive(true);
    }

    private void OnDisable()
    {
        _drawTool?.SetActive(false);
    }
}
