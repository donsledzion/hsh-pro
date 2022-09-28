using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    public Vector2 SnappedPoint = new Vector2(0f,0f);
    public Building Building;


    //here should be (for now at least) all bools that represents buttons and togglers status

    //Grid section

    public bool GridSnap = false;

    public bool AngleSnap = false;

    public bool DynamicDimensions = false;

    public bool WallPointSnap = true;
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

    public bool ToggleWallPointSnap()
    {
        WallPointSnap = !WallPointSnap;
        return WallPointSnap;
    }

    internal void AddCeilingToCurrentStorey(Vector2[] points)
    {
        Storey currentStorey = ins.Building.CurrentStorey;
        currentStorey.AddNewCeiling(new Ceiling(DefaultSettings.ins.CeilingThickness, currentStorey.Elevation + currentStorey.Height, points));
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
    public void AddStoreySimple()
    {
        Building.AddStoreySimple();

        Drawing2DController.ins.SwitchToStorey(Building.CurrentStorey);
    }

}
