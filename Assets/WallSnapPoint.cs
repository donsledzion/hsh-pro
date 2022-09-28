using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Selector2D;

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

    protected override Selector2D.Point2DInfo ClosestPoint(Vector2 mouseInput)
    {
        Point2DInfo closestPoint = base.ClosestPoint(mouseInput);
        float maxDist = Mathf.Min(_pointSnapDistance,(closestPoint.Point-mouseInput).magnitude);
        foreach (Vector2 point in Drawing2DController.ins.LinePoints)
        {
            if ((point - mouseInput).magnitude < maxDist)
            {
                maxDist = (point - mouseInput).magnitude;
                closestPoint = new Point2DInfo(point, new Walls2D.Wall());
            }
        }

        return closestPoint;
    }
}
