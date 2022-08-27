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

        protected virtual void Start()
        {
            _uIController = UIController.ins;
            _drawing2DController = Drawing2DController.ins;
            _angleSnapController = AngleSnapController.ins;
        }

        protected virtual void Update()
        {
            pointerPosition = Input.mousePosition;
            if (GameManager.ins.GridSnap)
                pointerPosition = _uIController.GridSnap(pointerPosition);

            if (GameManager.ins.AngleSnap)
                pointerPosition = _angleSnapController.AngleSnap(pointerPosition);

        }
    }
}