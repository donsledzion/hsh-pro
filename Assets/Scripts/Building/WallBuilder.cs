using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallBuilder : DrawOnCanvas
{
    List<WallSection> _wallSections = new List<WallSection>();
    [SerializeField] DynamicInputController _dynamicInputController;

    public bool IsDrawing { get; private set; }

    protected override void Update()
    {
        base.Update();
        if (GameManager.ins.PointerOverUI && IsDrawing)
            _drawing2DController.DrawLive(pointerPosition);

        if (Input.GetMouseButtonDown(0) && GameManager.ins.PointerOverUI)
        {
            if (!IsDrawing)
                    IsDrawing = true;
            _drawing2DController.AddLinePointWithLabel(pointerPosition, true);
            AddWallSection();
        }
        if (Input.GetMouseButtonDown(1) && GameManager.ins.PointerOverUI)
        {
            BrakeLine();
        }


        _dynamicInputController.DynamicInput();

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            int pointsCount = _drawing2DController.LinePointsCount;
            Vector2 lastPoint = _drawing2DController.LinePoints[pointsCount - 1];
            if (_dynamicInputController.DynamicInputLength > 0 && lastPoint != Vector2.zero)
            {
                _drawing2DController.ApplyDynamicInput(pointerPosition);
            }
        }
    }

    private void OnEnable()
    {
        IsDrawing = true;
    }

    private void OnDisable()
    {
        IsDrawing = false;
    }

    private void BrakeLine()
    {
        if (_drawing2DController.IsEmptyOrDefault()) return;
        IsDrawing = false;
        _drawing2DController.ClearLiveLine();
        Wall wall = _drawing2DController.ApplyWallToBuilding();
        _drawing2DController.StoreWall(wall);
        _drawing2DController.ClearCurrentLine();
    }

    public void AddWallSection()
    {
        Vector2[] allPoints = _drawing2DController.LinePoints;
        int allPointsCount = allPoints.Length;
        if (allPointsCount < 2) return;
        Vector2[] sectionPoints = { allPoints[allPointsCount - 2], allPoints[allPointsCount - 1] };
        WallSection section = new SectionStraight(sectionPoints);

        _wallSections.Add(section);
    }
}
