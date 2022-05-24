using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Walls2D
{
    public class ArcsBuilder : DrawOnCanvas
    {

        protected override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(0) && GameManager.ins.PointerOverUI)
            {
                _drawing2DController.SpawnPointLabel(pointerPosition, true);
            }
        }

    }
}