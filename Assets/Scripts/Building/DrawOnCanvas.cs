using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class DrawOnCanvas : MonoBehaviour
    {

        protected UIController _uIController;
        protected AngleSnapController _angleSnapController;
        protected Drawing2DController _drawing2DController;
        protected Vector3 pointerPosition;
        protected Vector2 snappedEndPoint => GameManager.ins.SnappedEndPoint;
        protected Vector2 snappedClosePoint => GameManager.ins.SnappedClosePoint;
        protected bool snappedEndPointFound => snappedEndPoint != new Vector2(0f, 0f);
        protected bool snappedClosePointFound => snappedClosePoint != new Vector2(0f, 0f);

        protected virtual void Start()
        {
            _uIController = UIController.ins;
            _drawing2DController = Drawing2DController.ins;
            _angleSnapController = AngleSnapController.ins;
        }

        protected virtual void Update()
        {            
            pointerPosition = Input.mousePosition;

            
            if (GameManager.ins.WallSectionEndSnap && snappedEndPointFound)
                pointerPosition = CanvasController.CanvasCoordsToScreenPoint(snappedEndPoint);
            else if (GameManager.ins.WallSectionCloseSnap && snappedClosePointFound)
            {
                pointerPosition = CanvasController.CanvasCoordsToScreenPoint(snappedClosePoint);
                GameManager.ins.SnappedClosePoint = pointerPosition;
            }
            
            if (GameManager.ins.GridSnap)
                pointerPosition = _uIController.GridSnap(pointerPosition);

            if (GameManager.ins.AngleSnap)
                pointerPosition = _angleSnapController.AngleSnap(pointerPosition);            
        }
    }
}