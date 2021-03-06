using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager ins { get; private set; }

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

    public Vector3 DrawingCanvasBackgroundLBCorner;
    public float Zoom;
    public bool PointerOverUI;
    public bool RelativeAngle = true;
    public Vector2 ResolutionRatio;
    public Building Building;


    //here should be (for now at least) all bools that represents buttons and togglers status

    //Grid section

    public bool GridSnap = false;

    public bool AngleSnap = false;

    public bool DynamicDimensions = false;
    public bool ToggleGridSnap()
    {
        GridSnap = !GridSnap;
        return GridSnap;
    }

    public bool ToggleRelativeAngle()
    {
        RelativeAngle = !RelativeAngle;
        return RelativeAngle;
    }

    public bool ToggleAngleSnap()
    {
        AngleSnap = !AngleSnap;
        return AngleSnap;
    }

    public bool ToggleDynamicDimensions()
    {
        DynamicDimensions = !DynamicDimensions;
        return DynamicDimensions;
    }

    [ContextMenu("Report Status")]
    void ReportStatus()
    {
        Debug.Log("GameManager status: Building: " + Building.ToString());
    }

    [ContextMenu("Add Storey Simple")]
    void AddStoreySimple()
    {
        Building.AddStoreySimple();
    }

}
