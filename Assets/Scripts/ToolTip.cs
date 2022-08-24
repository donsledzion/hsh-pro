using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler
{

    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipController._instance.ShowToolTip(message);
    }

}
