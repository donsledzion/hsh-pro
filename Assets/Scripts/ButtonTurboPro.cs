using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonTurboPro : Button
{
    private void Update()
    {
        if (IsHighlighted())
        {
            ToolTipController._instance.ShowToolTip("terere");
        }
        else /*if (ToolTipController._instance.gameObject.activeSelf)*/
        {
            ToolTipController._instance.HideToolTip();
        }
    }
}
