using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationWindowController : MonoBehaviour
{

    public static InformationWindowController _instance;

    public TextMeshProUGUI headerSection;
    public TextMeshProUGUI textToDisplaySection;


   public void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
        }
    }

    public void ShowToolTipTimer(string header, string textToDisplay)
    {
        gameObject.SetActive(true);
        headerSection.text = header;
        textToDisplaySection.text = textToDisplay;
        Invoke("HideToolTip", 3.0f);
    }

    public void ShowToolTip(string header, string textToDisplay)
    {

        gameObject.SetActive(true);
        headerSection.text = header;
        textToDisplaySection.text = textToDisplay;
    }

    public void HideToolTip()
    {

        gameObject.SetActive(false);
        headerSection.text = string.Empty;
        textToDisplaySection.text = string.Empty;
    }
}
