using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InformationTips : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public string header;
    public string textToDisplay;
    public GameObject infoWindow;

    public void OnPointerEnter(PointerEventData eventData)
    {

        infoWindow.SetActive(true);
        InformationWindowController._instance.ShowToolTip(header,textToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InformationWindowController._instance.HideToolTip();
    }


}
