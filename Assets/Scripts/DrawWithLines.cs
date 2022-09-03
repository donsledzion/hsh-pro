using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public abstract class DrawWithLines : DrawOnCanvas
{
    protected DynamicInputController _dynamicInputController;
    public bool IsDrawing { get; protected set; }

    protected override void Start()
    {
        base.Start();
        _dynamicInputController = DynamicInputController.ins;
    }

    protected override void Update()
    {
        base.Update();
        if (GameManager.ins.PointerOverUI && IsDrawing)
            _drawing2DController.DrawLive(pointerPosition);

        if (Input.GetMouseButtonDown(0) && GameManager.ins.PointerOverUI)
        {
            if (!IsDrawing)
                IsDrawing = true;
            _drawing2DController.AddLinePoint(CanvasController.ScreenPointToCanvasCoords(pointerPosition), true,false);
            HandleClick();
        }
        if (Input.GetMouseButtonDown(1) && GameManager.ins.PointerOverUI)
        {
            BreakLine();
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

    protected abstract void BreakLine();
    protected abstract void HandleClick();
}
