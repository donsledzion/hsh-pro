using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipController._instance.ShowToolTip(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController._instance.HideToolTip();
    }
}
