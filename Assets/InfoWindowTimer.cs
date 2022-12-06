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
    public Vector2 position;

    


    public void OnPointerClick(PointerEventData eventData)
    {
        Resolution resolution = Screen.currentResolution;
        position = new Vector2(resolution.width / 2, position.y);
        infoWindow.SetActive(true);

        

        InformationWindowController._instance.ShowToolTipTimer(header, textToDisplay, position);   
    }

/*    public void OnPointerDown(PointerEventData eventData)
    {
        InformationWindowController._instance.ShowToolTipTimer(header, textToDisplay);
    }*/
}
