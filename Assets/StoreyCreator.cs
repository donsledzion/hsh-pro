using System.Collections;
using UnityEngine;

public class StoreyCreator : MonoBehaviour
{
    [SerializeField] GameObject _drawWalls;
    [SerializeField] GameObject _drawArcs;
    [SerializeField] GameObject _wallTypeSelector;
    [SerializeField] GameObject _selector2D;
    [SerializeField] GameObject _wallSectionDeleter;
    [SerializeField] GameObject _drawCeiling;
    [SerializeField] GameObject _drawDoor;
    [SerializeField] GameObject _drawWindow;

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

    public void DrawCeiling()
    {
        SwitchOffAll();
        _drawCeiling.SetActive(true);
    }

    public void SelectionTool()
    {
        SwitchOffAll();
        _selector2D.SetActive(true);
    }

    public void WallSectionDeleteTool()
    {
        SwitchOffAll();
        _wallSectionDeleter.SetActive(true);
    }

    public void DrawDoorTool()
    {
        SwitchOffAll();
        _drawDoor.SetActive(true);
    }

    public void DrawWindowTool()
    {
        SwitchOffAll();
        _drawWindow.SetActive(true);
    }

    public void SwitchOffAll()
    {
        _wallTypeSelector.SetActive(false);
        _drawWalls.SetActive(false);
        _drawCeiling.SetActive(false);
        _drawArcs.SetActive(false);
        _selector2D.SetActive(false);
        _wallSectionDeleter.SetActive(false);
        _drawDoor.SetActive(false);
        _drawWindow.SetActive(false);
    }
}
