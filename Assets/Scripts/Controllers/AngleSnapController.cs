using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Walls2D;
public class AngleSnapController : MonoBehaviour
{    
    [SerializeField] Slider _angleSnapSlider;
    [SerializeField] Drawing2DController _drawing2DController;

    public Vector3 AngleSnap(Vector3 pointerPosition)
    {
        if (_drawing2DController.IsEmptyOrDefault()) return pointerPosition;

        float zoom = GameManager.ins.Zoom;
        Vector2 lbCorner = GameManager.ins.DrawingCanvasBackgroundLBCorner;

        int pointsCount = _drawing2DController.LinePointsCount;
        float angleSnap = _angleSnapSlider.value;

        Vector2 targetPos = pointerPosition;

        Vector2 globalVector = Vector2.right;
        Vector2 lastPoint = lbCorner + _drawing2DController.LinePoints[pointsCount - 1] * zoom;
        Vector2 currentVector = targetPos - lastPoint;

        float angle = Vector2.SignedAngle(currentVector, globalVector);


        float snappedAngle = angleSnap * Mathf.Round(angle / angleSnap);
        Debug.Log("Angle: " + angle + " | Snapped: " + snappedAngle);

        float radius = currentVector.magnitude;

        float x = lastPoint.x + radius * Mathf.Cos(snappedAngle * Mathf.PI / 180f);
        float y = lastPoint.y - radius * Mathf.Sin(snappedAngle * Mathf.PI / 180f);

        Vector3 resVec = new Vector3(x, y, 0);

        return resVec;
    }
}
