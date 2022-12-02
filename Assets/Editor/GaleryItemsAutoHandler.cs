using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GaleryItemsAutoHandler : Editor
{
    [MenuItem("Assets/AutoAssignGIO")]
    public static void AutoAssignReferences()
    {
        Type clickedType = Selection.activeObject.GetType();

        if((clickedType != null) && (clickedType == typeof(ScriptableObjectsController)) )
        {
            ScriptableObjectsController soc = Selection.activeObject as ScriptableObjectsController;
            //soc.imagePreview =
            //soc.prefab;
            //soc.material = 
            //soc.Description = 
            //soc.name = soc.prefab.name;
            //soc.collectionName = "default collecion";
            //soc.tiling_x 100;
            //soc.tiling_y = 100;
            Debug.Log("Great! We're here!");
            
        }

    }
    
}
