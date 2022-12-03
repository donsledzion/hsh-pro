using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Unity.Plastic.Newtonsoft.Json.Schema;

public class GaleryItemsAutoHandler : Editor
{
    [MenuItem("Assets/AutoAssignGIO")]
    public static void AutoAssignReferences()
    {
        Type clickedType = Selection.activeObject.GetType();

        if((clickedType != null) && (clickedType == typeof(ScriptableObjectsController)) )
        {
            ScriptableObjectsController soc = Selection.activeObject as ScriptableObjectsController;

            string assetPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
            //Debug.Log("Path: " + assetPath);
            string relativePath = assetPath.Substring(0,assetPath.Length-Selection.activeObject.name.Length-6);
            //Debug.Log("RelativePath-1: " + relativePath);
            string dirPath = Path.GetDirectoryName(assetPath);
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            //Debug.Log("DirInfo: " + dirPath);
            FileInfo[] filesInfo = dirInfo.GetFiles();


            foreach(FileInfo file in filesInfo)
            {
                if(!(file.Extension.ToString()).Contains("meta"))
                {
                    Debug.Log(file.ToString());                    
                    /*if(file.Extension.ToString().Contains("prefab"))
                    {
                        Debug.Log("Trying to load GO: "+ file.Name);
                        soc.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(Path.Combine(relativePath , file.Name), typeof(GameObject)); 
                    }*/
                    UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(Path.Combine(relativePath, file.Name));
                    if (assets[0].GetType() == typeof(GameObject))
                    {
                        soc.prefab = assets[0] as GameObject;
                        soc.name = ((GameObject)(assets[0])).name;
                        soc.collectionName = ((GameObject)(assets[0])).name + "_collection";
                    }
                    else if (assets[0].GetType() == typeof(Texture2D))
                    {
                        foreach(UnityEngine.Object asset in assets)
                            if(asset.GetType() == typeof(Sprite))
                            {
                                soc.imagePreview = asset as Sprite;
                            }
                        

                    }
                    else if (assets[0].GetType() == typeof(TextAsset)) soc.Description = assets[0] as TextAsset;
                    else if (assets[0].GetType() == typeof(Material)) soc.material = assets[0] as Material;                    
                    
                    soc.tiling_x = 100f;
                    soc.tiling_y = 100f;

                    if(soc.Description == null)
                    {
                        TextAsset text = new TextAsset();
                        AssetDatabase.CreateAsset(text, Path.Combine(relativePath,"Description.txt"));
                        soc.Description = text;
                    }
                }
                    

            }
            //soc.imagePreview =
            //soc.prefab;
            //soc.material = 
            //soc.Description = 
            //soc.name = soc.prefab.name;
            //soc.collectionName = "default collecion";
            //soc.tiling_x 100;
            //soc.tiling_y = 100;
            //Debug.Log("Great! We're here!");
            
        }

    }
    
}
