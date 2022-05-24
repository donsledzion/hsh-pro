using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls2DInputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GameManager.ins.ToggleRelativeAngle();
        if (Input.GetKeyDown(KeyCode.S))
            GameManager.ins.ToggleGridSnap();
        if (Input.GetKeyDown(KeyCode.O))
            GameManager.ins.ToggleAngleSnap();
        if (Input.GetKeyDown(KeyCode.D))
            GameManager.ins.ToggleDynamicDimensions();

        
    }
}
