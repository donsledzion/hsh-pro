using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreyCreator : MonoBehaviour
{
    [SerializeField] GameObject _drawWalls;
    [SerializeField] GameObject _drawArcs;

    public void DrawWalls()
    {
        SwitchOffAll();
        _drawWalls.SetActive(true);
    }

    public void DrawArcs()
    {

        SwitchOffAll();
        _drawArcs.SetActive(true);
    }

    private void SwitchOffAll()
    {
        _drawWalls.SetActive(false);
        _drawArcs.SetActive(false);
    }
}
