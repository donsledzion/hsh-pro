using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Walls2D;
using static Selector2D;

public class WallBuilder : DrawWithLines
{
    List<WallSection> _wallSections = new List<WallSection>();

    [SerializeField] WallSectionSnapClosePoint _wallSectionSnapClosePoint;
    [SerializeField] GameObject _gridDot;
    [SerializeField] GameObject _wallThicknessDialogPrefab;
    WallThicknessDialogLabel _wallThicknessDialogLablel;

    bool _rightMouseClickCooldown = false;

    protected override void Update()
    {
        base.Update();
        
        if (_wallThicknessDialogLablel != null &&  ((Input.GetMouseButtonDown(1) && _rightMouseClickCooldown == false )|| Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            Debug.Log("<color=red>Applying thickness!</color>");
            AddWallWithThickness(_wallThicknessDialogLablel.ValidateInput());
            Destroy(_wallThicknessDialogLablel.gameObject);
            _wallThicknessDialogLablel = null;
            _waitingForConfirm = false;
        }
    }

    private void OnEnable()
    {
        IsDrawing = true;
        _wallSectionSnapClosePoint.AllowJambs = false;
    }

    protected override void OnDisable()
    {
        IsDrawing = false;
        base.OnDisable();
        _wallSectionSnapClosePoint.AllowJambs = true;
    }

    protected override void BreakLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        IsDrawing = false;        
        _drawing2DController.ClearLiveLine();

        if (_drawing2DController.LinePointsCount < 2)
        {
            _drawing2DController.ClearCurrentLine();
            return;
        }
        //========================================================================================
        ReadWallThickness();
        //========================================================================================

        /*Wall wall = _drawing2DController.ApplyWallToBuilding();
        _drawing2DController.StoreWall(wall);
        CheckForLinesToBreak();
        _drawing2DController.ClearCurrentLine();*/
        
    }

    public void AddWallWithThickness(float thickness)
    {
        _drawing2DController.CurrentWallThickness = thickness;
        Wall wall = _drawing2DController.ApplyWallToBuilding();
        _drawing2DController.StoreWall(wall);
        CheckForLinesToBreak();
        _drawing2DController.ClearCurrentLine();
        
        
    }

    IEnumerator ReleaseRightClickCor()
    {
        yield return new WaitUntil(()=>Input.GetMouseButtonUp(1));  
        _rightMouseClickCooldown = false;
    }

    void ReadWallThickness()
    {
        GameObject wallThicknessDialog = Instantiate(_wallThicknessDialogPrefab, transform);
        wallThicknessDialog.transform.localPosition = _drawing2DController.LinePoints[_drawing2DController.LinePoints.Length - 1];
        _wallThicknessDialogLablel = wallThicknessDialog.GetComponent<WallThicknessDialogLabel>();
        _waitingForConfirm = true;
        if (Input.GetMouseButton(1))
        {
            _rightMouseClickCooldown = true;
            StartCoroutine(ReleaseRightClickCor());
        }
    }

    private void CheckForLinesToBreak()
    {
        Vector2[] storeyPoints = GameManager.ins.Building.CurrentStorey.WallSectionPoints;
        List<WallSection> sections = GameManager.ins.Building.CurrentStorey.SectionsOfType(typeof(SectionStraight));
        foreach(Vector2 point in storeyPoints)
        {
            foreach(WallSection section in sections)
            {
                if((ClosestSection(point,10f) == section) && section.PointAwayFromEdges(point))
                {
                    if (section.SplitSection(point))
                        CheckForLinesToBreak();
                    return;
                }
            }
        }
        Drawing2DController.ins.RedrawCurrentStorey();
    }

    protected override void HandleClick()
    {

    }

    public WallSection ClosestSection(Vector2 point, float minDistanceFromEdge = 0f)
    {
        float lineSnapDistance = 1f;
        List<LineSection> segments = new List<LineSection>();
        List<Wall> walls = GameManager.ins.Building.CurrentStorey.Walls;
        WallSection closestSection = null;
        float closestDistance = lineSnapDistance;
        foreach (Wall wall in walls)
        {
            foreach (WallSection section in wall.WallSections)
            {
                LineSection lSection = new LineSection(section);
                segments.Add(lSection);
                float distance = MathHelpers.PointToLineDistance(lSection.Start, lSection.End, point);
                bool pointWithinSection = MathHelpers.DoesPointCastsOnLine(lSection.Start, lSection.End, point);
                if ((distance < closestDistance) 
                    && pointWithinSection 
                    && (((point - section.StartPoint.Position).magnitude >= minDistanceFromEdge)
                        && (point - section.EndPoint.Position).magnitude >= minDistanceFromEdge))
                {
                    closestDistance = distance;
                    closestSection = section;
                }
            }
        }
        if (closestSection != null)
            Debug.Log("Closest section wall: " + closestSection.Wall);
        return closestSection;
    }
}
