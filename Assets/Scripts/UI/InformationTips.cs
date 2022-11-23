using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InformationTips : MonoBehaviour, IPointerEnterHandler
{
    public string header;
    public string textToDisplay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        InformationWindowController._instance.ShowToolTip(header,textToDisplay);
    }
}
