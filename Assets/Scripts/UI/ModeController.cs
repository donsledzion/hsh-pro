using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;
using UnityEngine.EventSystems;
 

public class ModeController : MonoBehaviour
{

    public static ModeController ins { get; private set; }
    
    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    [SerializeField] List<GameObject> listOf2DObjects = new List<GameObject>();
    [SerializeField] List<GameObject> listOf3DObjects = new List<GameObject>();
    [SerializeField] List<GameObject> listOfMenuObjects = new List<GameObject>();
    [SerializeField] List<GameObject> listOfDecorationObjects = new List<GameObject>();
    [SerializeField] List<GameObject> MenuOfFurnitureController = new List<GameObject>();
    [SerializeField] List<GameObject> listOfFinishingObjects = new List<GameObject>();
    [SerializeField] List<GameObject> VRMOde = new List<GameObject>();

    [SerializeField] SimpleCameraController cameraController;
    [SerializeField] GameObject builder3D;
    [SerializeField] GameObject galeryItemsWindow;
    Builder3D refToScript;
    Item3DViewer item3DViewer;
    [SerializeField] GameObject settingsWindowOfMainMenu;
    [SerializeField] GameObject exitWindowOfMainMenu;
    [SerializeField] GameObject itemDescriptionWindow;
    [SerializeField] GameObject prefatToFit;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject furnitureSelection;


    public void SelectionMode() {

        furnitureSelection.SetActive(true);
    }

    public void VRMode()
    {

        ResetAllListObjects();
        mainCamera.SetActive(false);
//        refToScript = builder3D.GetComponent<Builder3D>();
//        refToScript.GenerateBuilding();

        foreach (GameObject VrObjects in VRMOde)
        {
            VrObjects.SetActive(true);
        }
    }

    public void Mode2D() {

        ResetAllListObjects();
        cameraController.enabled = false;
        foreach (GameObject objects2D in listOf2DObjects)
        {
            objects2D.SetActive(true);

            switch (objects2D.name)
            {
                case "Main Body Layout":

                    objects2D.GetComponent<Image>().enabled = true;

                break;

                case "Left Side":

                    objects2D.GetComponent<Image>().enabled = true;

                break;

                default:
                    break;
            }
        }
    }

    public void Mode3D()
    {
        ResetAllListObjects();

        cameraController.enabled = true;
        refToScript = builder3D.GetComponent<Builder3D>();
        refToScript.GenerateBuilding();        

        foreach (GameObject objects3D in listOf3DObjects)
        {
            objects3D.SetActive(true);
        }
            
    }

    public void FinishingMode() {

        ResetAllListObjects();
        foreach (GameObject objects3D in listOfFinishingObjects)
        {
            objects3D.SetActive(true);
        }
    }

    public void ModeMenu() {

        ResetAllListObjects();

        foreach (GameObject objectsMenu in listOfMenuObjects)
        {
            objectsMenu.SetActive(true);

            cameraController.enabled = false;

            switch (objectsMenu.name)
            {
                case "Main Body Layout":

                    objectsMenu.GetComponent<Image>().enabled = true;

                    break;

                case "Left Side":

                    objectsMenu.GetComponent<Image>().enabled = true;

                    break;

                default:
                    break;
            }
        }
    }

    public void DeocorationMode() {

        ResetAllListObjects();
        ResetFurnitureTabs();
        cameraController.enabled = true;
//        refToScript = builder3D.GetComponent<Builder3D>();
//        refToScript.GenerateBuilding();

        foreach (GameObject objects3D in listOfDecorationObjects)
        {
            objects3D.SetActive(true);
        }
    }

    public void MainMenuOfFurniture_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Main Tab Area") furnitureTab.SetActive(true);
        }
    }

    public void MenuOfSoftFurniture_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Soft Furniture Tab Area") {
                furnitureTab.SetActive(true);
            }
        }

    }

    public void MenuOfFurniture_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Furniture Tab Area") furnitureTab.SetActive(true);
        }
    }

    public void FurnituresTabs()
    {   
        itemDescriptionWindow.SetActive(false);
        galeryItemsWindow.SetActive(true);
        prefatToFit.SetActive(false);
    }

    public void ResetFurnitureTabs() {

        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            
            furnitureTab.SetActive(false);

            if (furnitureTab.GetComponent<TabGroup>()) {

                furnitureTab.GetComponent<TabGroup>().selectedTab = null;
                Debug.Log(furnitureTab.name);

            }else if(furnitureTab.GetComponent<ToolsTabGroup>())
            {
                furnitureTab.GetComponent<ToolsTabGroup>().selectedTab = null;
            }
        }
    }

    private void ResetAllListObjects()
    {

        galeryItemsWindow.SetActive(false);
        settingsWindowOfMainMenu.SetActive(false);
        exitWindowOfMainMenu.SetActive(false);
        itemDescriptionWindow.SetActive(false);
        prefatToFit.SetActive(false);
        mainCamera.SetActive(true);
        furnitureSelection.SetActive(false);
        
        foreach (GameObject objects2D in listOf2DObjects)
        {
            objects2D.SetActive(false);
            switch (objects2D.name)
            {

                case "Main Body Layout":

                    objects2D.GetComponent<Image>().enabled = false;

                    break;

                case "Left Side":

                    objects2D.GetComponent<Image>().enabled = false;

                    break;

                default:
                    break;
            }

        }

        foreach (GameObject objects3D in listOf3DObjects)
        {
            objects3D.SetActive(false);
        }

        foreach (GameObject decorationObject in listOfDecorationObjects)
        {
            decorationObject.SetActive(false);

            if (decorationObject.name == "Furniture Tools") {

                decorationObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().enabled = false;
            
            }
        }

        foreach (GameObject VrObjects in VRMOde)
        {
             VrObjects.SetActive(false);
        }

        foreach (GameObject objects3D in listOfFinishingObjects)
        {
            objects3D.SetActive(false);
        }
    }
}
