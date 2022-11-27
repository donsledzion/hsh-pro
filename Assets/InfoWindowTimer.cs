using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoWindowTimer : MonoBehaviour,IPointerClickHandler
{
    public string header;
    public string textToDisplay;
    public GameObject infoWindow;
    public GameObject buttonRef;


    public void OnPointerClick(PointerEventData eventData)
    {
        infoWindow.SetActive(true);
        InformationWindowController._instance.ShowToolTipTimer(header, textToDisplay);   
    }

/*    public void OnPointerDown(PointerEventData eventData)
    {
        InformationWindowController._instance.ShowToolTipTimer(header, textToDisplay);
    }*/
}
