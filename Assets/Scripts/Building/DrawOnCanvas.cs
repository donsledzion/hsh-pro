using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walls2D
{
    public class DrawOnCanvas : MonoBehaviour
    {

        [SerializeField] protected UIController uIController;
        [SerializeField] protected AngleSnapController _angleSnapController;
        [SerializeField] protected Drawing2DController _drawing2DController;
        protected Vector3 pointerPosition;

        protected virtual void Update()
        {
            pointerPosition = Input.mousePosition;
            if (GameManager.ins.GridSnap)
                pointerPosition = uIController.GridSnap(pointerPosition);

            if (GameManager.ins.AngleSnap)
                pointerPosition = _angleSnapController.AngleSnap(pointerPosition);

        }
    }
}