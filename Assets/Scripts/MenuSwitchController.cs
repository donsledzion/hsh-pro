using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuSwitchController : MonoBehaviour
{

    [SerializeField]
    private Button twoDViewButton;

    [SerializeField]
    private Button threeDViewButton;

    [SerializeField]
    private Button buildViewButton;

    [SerializeField]
    private Button finishOfViewButton;    
    
    [SerializeField]
    private Button decorationViewButton;    
    
    [SerializeField]
    private Button elementsCreatorViewButton;

    [SerializeField]
    private Button vrViewButton;

    [SerializeField]
    private Transform leftMenuTransform;

    //public  List<GameObject> listOfViewOptions;

    [SerializeField]
    GameObject LeftMenuPrefab;




    private void twoDSelected()
    {
        string nameOfMenuPrefab = "Menu 2D";
        
        showMenu();
    }


    public void showMenu( )
    {

        /*        listOfViewOptions = new List<GameObject>(Resources.LoadAll<GameObject>("LeftSideMenus"));

                int menuIndex = listOfViewOptions.FindIndex(gameObject => string.Equals(nameOfMenuPrefab, gameObject.name));

                Debug.Log(menuIndex);

                Instantiate(listOfViewOptions[menuIndex+1].transform.SetParent(lefMenuTransform));
        */


        GameObject LeftMenu = Instantiate(LeftMenuPrefab, leftMenuTransform);



    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
