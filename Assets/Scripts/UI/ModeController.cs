using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;


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

    public List<GameObject> listOf2DObjects = new List<GameObject>();
    public List<GameObject> listOf3DObjects = new List<GameObject>();
    public List<GameObject> listOfMenuObjects = new List<GameObject>();

    public SimpleCameraController _cameraController;
    public GameObject _builder3D;
    Builder3D refToScript;

    public void Mode2D() {

        ResetAllListObjects();
        foreach(GameObject objects2D in listOf2DObjects)
        {
            objects2D.SetActive(true);

            _cameraController.enabled = false;

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

        _cameraController.enabled = true;
        refToScript = _builder3D.GetComponent<Builder3D>();
        refToScript.GenerateBuilding();
        
        

        foreach (GameObject objects3D in listOf3DObjects)
        {
            objects3D.SetActive(true);
 
        }
            
    }

    public void ModeMenu() {

        ResetAllListObjects();

        foreach (GameObject objectsMenu in listOfMenuObjects)
        {
            objectsMenu.SetActive(true);

            _cameraController.enabled = false;

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

    private void ResetAllListObjects()
    {
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
    }
}
