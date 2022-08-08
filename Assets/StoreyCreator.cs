using System.Collections;
using UnityEngine;

public class StoreyCreator : MonoBehaviour
{
    [SerializeField] GameObject _drawWalls;
    [SerializeField] GameObject _drawArcs;

    public static StoreyCreator ins { get; private set; }

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

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

    public void SwitchOffAll()
    {
        _drawWalls.SetActive(false);
        _drawArcs.SetActive(false);
    }
}
