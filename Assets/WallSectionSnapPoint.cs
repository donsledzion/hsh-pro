using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSectionSnapPoint : Selector2D
{
    Vector2 _snappedPoint;

    protected override void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        _hoveredSection = ClosestSection(mouseOverCanvas);
        if (_hoveredSection != null)
        {
            _snappedPoint = CastedPoint(_hoveredSection, mouseOverCanvas);
            GameManager.ins.SnappedClosePoint = HoverPoint(_snappedPoint, _hoverColor);            
        }
        else
        {
            UnhoverPoint();
            GameManager.ins.SnappedClosePoint = new Vector2(0f, 0f);
        }
    }
}
