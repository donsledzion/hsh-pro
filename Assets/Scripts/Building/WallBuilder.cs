using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallBuilder : DrawOnCanvas
{
    List<WallSection> _wallSections = new List<WallSection>();
    [SerializeField] DynamicInputController _dynamicInputController;


    protected override void Update()
    {
        base.Update();

        if (GameManager.ins.PointerOverUI)
            _drawing2DController.DrawLive(pointerPosition);

        if (Input.GetMouseButtonDown(0) && GameManager.ins.PointerOverUI)
        {
            _drawing2DController.AddLinePointWithLabel(pointerPosition, true);
            AddWallSection();
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
