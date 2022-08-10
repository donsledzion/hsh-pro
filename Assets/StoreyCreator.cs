using System.Collections;
using UnityEngine;

public class StoreyCreator : MonoBehaviour
{
    [SerializeField] GameObject _drawWalls;
    [SerializeField] GameObject _drawArcs;
    [SerializeField] GameObject _wallTypeSelector;
    [SerializeField] GameObject _selector2D;

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
        _wallTypeSelector.SetActive(true);
        _wallTypeSelector.GetComponent<WallInfoDisplay>().UpdateInfo();
    }

    public void DrawArcs()
    {

        SwitchOffAll();
        _drawArcs.SetActive(true);
    }

    public void SelectionTool()
    {
        SwitchOffAll();
        _selector2D.SetActive(true);
    }

    public void SwitchOffAll()
    {
        _wallTypeSelector.SetActive(false);
        _drawWalls.SetActive(false);
        _drawArcs.SetActive(false);
        _selector2D.SetActive(false);
    }
}
