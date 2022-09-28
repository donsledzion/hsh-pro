using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSnapPoint : Point2DSelector
{
    Color _hoverColor = Color.blue;
    Color _selectingColor = Color.red;
    Color _defaultColor = Color.black;

    private void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        GameManager.ins.SnappedPoint = HoverPoint(ClosestPoint(mouseOverCanvas).Point, _hoverColor);
    }
}
