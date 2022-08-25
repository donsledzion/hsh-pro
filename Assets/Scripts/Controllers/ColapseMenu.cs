using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ColapseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject dropDownMenu;

    [SerializeField]
    GameObject toolTipBox;

    [SerializeField]
    private bool _isOn = false;


    public void collapse()
    {
        
        show(_isOn);
    }


    private void show (bool value)
    {
        
        if (value)
        {
            dropDownMenu.SetActive(false);
            toolTipBox.SetActive(false);
            _isOn = false;
            
        }
        else
        {
            dropDownMenu.SetActive(true);
            toolTipBox.SetActive(true);
            _isOn = true;
        }

    }

    public void hide()
    {
        dropDownMenu.SetActive(false);
        toolTipBox.SetActive(false);
        _isOn = false;
    }
}
