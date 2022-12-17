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
    public Vector2 position;

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Resolution resolution = Screen.currentResolution;
        position = new Vector2(resolution.width / 2, position.y);
        infoWindow.SetActive(true);
        InformationWindowController.Instance.ShowToolTip(header,textToDisplay,position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InformationWindowController.Instance.HideToolTip();
    }


}
