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
    private bool _isOn = false;

    public void colapse()
    {
        show(_isOn);
    }


    private void show (bool value)
    {
        Debug.Log("this is value " + value);
        if (value)
        {
            Debug.Log("this is value " + value);
            dropDownMenu.SetActive(false);
            _isOn = false;
        }
        else
        {
            Debug.Log("this is value " + value);
            dropDownMenu.SetActive(true);
            _isOn = true;
        }

    }
}
