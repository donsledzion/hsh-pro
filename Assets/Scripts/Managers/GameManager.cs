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

    public float SnapCornersDistance = 5f;

    public Vector3 DrawingCanvasBackgroundLBCorner { get; private set; }
    public float Zoom;
    public bool PointerOverUI;
    public bool RelativeAngle = true;
    public Vector2 ResolutionRatio { get; private set; }
    public Vector2 SnappedEndPoint = new Vector2(0f,0f);
    public Vector2 SnappedClosePoint = new Vector2(0f,0f);
    public Building Building;
    public Camera CurrentCamera { get; set; }

    //here should be (for now at least) all bools that represents buttons and togglers status

    //Grid section

    public bool GridSnap = false;

    public bool AngleSnap = false;

    public bool DynamicDimensions = false;

    public bool WallSectionEndSnap = true;

    public bool WallSectionCloseSnap = true;

    public bool WallOrderDebugMode = false;

    public bool CornersFinishingDebugMode = true;
    
    public void SetLBCorner(Vector3 corner)
    {
        DrawingCanvasBackgroundLBCorner = corner;
    }
    
    public void SetResolutionRatio(Vector2 ratio)
    {
        ResolutionRatio = ratio;
    }

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
        WallSectionEndSnap = !WallSectionEndSnap;
        return WallSectionEndSnap;
    }

    internal void AddCeilingToCurrentStorey(Vector2[] points)
    {
        Storey currentStorey = ins.Building.CurrentStorey;
        currentStorey.AddNewCeiling(new Ceiling(DefaultSettings.ins.CeilingThickness, currentStorey.Elevation + currentStorey.Height, points));
    }

    internal FloorSection2D AddFloorSectionToCurrentStorey(Vector2[] points, int order)
    {
        return Building.CurrentStorey.AddNewFloorSection(
            new FloorSection2D(order , Building.CurrentStorey.Elevation, points));
    }

    public bool ToggleAngleSnap()
    {
        AngleSnap = !AngleSnap;
        return AngleSnap;
    }

    public bool ToggleDynamicDimensions()
    {
        DynamicDimensions = !DynamicDimensions;
        if (!DynamicDimensions) DynamicInputController.ins.DisposeOfLabel();
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
