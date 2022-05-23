using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls2DInputHandler : MonoBehaviour
{

    [SerializeField] UIController uIController;
    [SerializeField] AngleSnapController _angleSnapController;
    [SerializeField] Drawing2DController _drawing2DController;
    [SerializeField] DynamicInputController _dynamicInputController;

    [SerializeField] WallBuilder _wallBuilder;

    void Update()
    {
        Vector3 pointerPosition = Input.mousePosition;
        if (GameManager.ins.GridSnap)
            pointerPosition = uIController.GridSnap(pointerPosition);

        if (GameManager.ins.AngleSnap)
            pointerPosition = _angleSnapController.AngleSnap(pointerPosition);

        if (Input.GetMouseButtonDown(0) && GameManager.ins.PointerOverUI)
        {
            _drawing2DController.SpawnPointLabel(pointerPosition, true);
            _wallBuilder.AddWallSection();
        }

        if (Input.GetKeyDown(KeyCode.A))
            GameManager.ins.ToggleRelativeAngle();

        if (GameManager.ins.PointerOverUI)
            _drawing2DController.DrawLive(pointerPosition);

        if (Input.GetKeyDown(KeyCode.S))
            GameManager.ins.ToggleGridSnap();
        if (Input.GetKeyDown(KeyCode.O))
            GameManager.ins.ToggleAngleSnap();
        if (Input.GetKeyDown(KeyCode.D))
            GameManager.ins.ToggleDynamicDimensions();

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
}
