using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipController : MonoBehaviour
{
    public static ToolTipController _instance;

    public TextMeshProUGUI textComponent;

    public void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public void Update()
    {
        //transform.position = Input.mousePosition;
    }

    public void ShowToolTip(string message)
    {

        gameObject.SetActive(true);
        textComponent.text = message;
       

    }

    public void HideToolTip()
    {

        gameObject.SetActive(false);
        textComponent.text = string.Empty;

    }
}
