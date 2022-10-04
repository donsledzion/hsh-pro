using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public SimpleCameraController cameraController;

    public void Mode2D() { 
    
        foreach(GameObject objects2D in listOf2DObjects)
        {
            objects2D.SetActive(true);
            if (objects2D.name == "Main Camera") {

                cameraController = objects2D.GetComponent<SimpleCameraController>();
                //cameraController.isActiveAndEnabled(false);
            }
        }
    
    }


}
