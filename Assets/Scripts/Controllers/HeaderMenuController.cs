using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool _isOn = false;


    public void collapse()
    {

        hide();
        show();
    }


    private void show()
    {
            gameObject.SetActive(true);

    }

    private void hide()
    {
        twoDMenu.SetActive(false);
        mainMenu.SetActive(false);        
    }

    private void Update()
    {

        /*Button activeButtonTwoD = GameObject.Find("2D View").GetComponent<Button>();
        Button activeButtonThreeD = GameObject.Find("3D View").GetComponent<Button>();*/
        //Button activeButtonMainMenu = GameObject.Find("Menu").GetComponent<Button>();

        /*if (twoDMenu.activeSelf)
            activeButtonTwoD.Select();*/

        /*if (mainMenu.activeSelf)
            activeButtonMainMenu.Select();*/


    }
}
