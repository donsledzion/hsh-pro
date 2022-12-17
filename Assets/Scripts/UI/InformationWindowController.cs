using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationWindowController : MonoBehaviour
{
    public static InformationWindowController Instance;

    public TextMeshProUGUI headerSection;
    public TextMeshProUGUI textToDisplaySection;
    [SerializeField]
    GameObject _windowGO;

   public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
        }
    }

    public void ShowToolTipTimer(string header, string textToDisplay, Vector2 position)
    {
        Debug.Log(gameObject.name);
        Debug.Log(headerSection.name);
        Debug.Log(textToDisplaySection.name);

        _windowGO.SetActive(true);

        headerSection.text = header;
        textToDisplaySection.text = textToDisplay;
        transform.position = new Vector3(position.x, position.y);
        Invoke("HideToolTip", 3.0f);

    }

    public void ShowToolTip(string header, string textToDisplay, Vector2 position /*, float hideAfterTime=0f*/)
    {

        _windowGO.SetActive(true);
        headerSection.text = header;
        textToDisplaySection.text = textToDisplay;
        transform.position = new Vector3(position.x, position.y);
        //if(hideAfterTime>0) Invoke("HideToolTip",hideAfterTime);
    }

    public void HideToolTip()
    {

        _windowGO.SetActive(false);
        headerSection.text = string.Empty;
        textToDisplaySection.text = string.Empty;
    }
}
