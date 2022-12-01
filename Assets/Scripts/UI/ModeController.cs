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
    [SerializeField] List<GameObject> surfaceSelectionMode = new List<GameObject>();
    [SerializeField] List<GameObject> DrawingTools = new List<GameObject>();
    [SerializeField] List<GameObject> LightObjects = new List<GameObject>();


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
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject saveWindow;
    [SerializeField] GameObject loadWindow;
    [SerializeField] GameObject headerPanel;
    [SerializeField] ToolsTabButton twoDHeader;

    public void FurnitureSelectionMode() {

        foreach (GameObject surface in surfaceSelectionMode)
        {
            if(surface.name == "EquipmentSelectionController")
            surface.SetActive(true);
        }
    }

    public void VRMode()
    {

        ResetAllListObjects();
        mainCamera.SetActive(false);
        canvas.GetComponent<Canvas>().enabled = false;
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
        headerPanel.GetComponent<ToolsTabGroup>().onTabSelected(twoDHeader);
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
        canvas.GetComponent<Canvas>().enabled = false;
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
        canvas.GetComponent<Canvas>().enabled = false;
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
        canvas.GetComponent<Canvas>().enabled = false;
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
            Debug.Log("Menu Main Tab Area");
            if (furnitureTab.name == "Menu Main Tab Area") furnitureTab.SetActive(true);
        }
    }

    public void MenuOfSoftFurniture_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Menu Soft Furniture Tab Area" || furnitureTab.name == "Galery Soft Furniture") {
                furnitureTab.SetActive(true);
            }
        }

    }

    public void MenuOfFurniture_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Menu Furniture Tab Area") furnitureTab.SetActive(true);
        }
    }

    public void MenuOfBoardFurniture_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Menu Board Furniture" || furnitureTab.name == "Galery Board Furniture")
            {
                furnitureTab.SetActive(true);
            }
        }

    }

    public void MenuOfLights_On()
    {
        ResetFurnitureTabs();
        foreach (GameObject furnitureTab in MenuOfFurnitureController)
        {
            if (furnitureTab.name == "Menu Lights" || furnitureTab.name == "Galery Lights")
            {
                furnitureTab.SetActive(true);
            }
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
                //Debug.Log(furnitureTab.name);

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
        canvas.GetComponent<Canvas>().enabled = true;
        saveWindow.SetActive(false);
        loadWindow.SetActive(false);

        foreach (GameObject drawingToolsObjects in DrawingTools)
        {

            drawingToolsObjects.SetActive(false);

        }

        foreach (GameObject MainMenuObjects in listOfMenuObjects)
        {

            MainMenuObjects.SetActive(false);

        }

        foreach (GameObject surface in surfaceSelectionMode)
        {

            surface.SetActive(false);

        }

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
