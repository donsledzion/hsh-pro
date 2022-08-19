using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject twoDMenu;

    [SerializeField]
    GameObject threeDMenu;

    [SerializeField]
    GameObject buildMenu;

    [SerializeField]
    GameObject finishOfMenu;

    [SerializeField]
    GameObject decorationMenu;

    [SerializeField]
    GameObject creatorMenu;

    [SerializeField]
    GameObject vrMenu;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    private bool _isOn = false;


    public void collapse()
    {

        hide();
        show(_isOn);
        
    }


    private void show(bool value)
    {

        if (value)
        {
            gameObject.SetActive(false);
            _isOn = false;

        }
        else
        {
            gameObject.SetActive(true);
            _isOn = true;
        }

    }

    private void hide()
    {
        twoDMenu.SetActive(false);
        mainMenu.SetActive(false);
        _isOn = false;
    }
}
