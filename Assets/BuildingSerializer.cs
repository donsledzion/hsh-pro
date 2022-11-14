using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuildingSerializer : MonoBehaviour
{
    [ContextMenu("Serialize Building")]
    void SerializeBuilding(string path)
    {
        GameManager.ins.Building.SerializeToXML(path);
    }

    void DeserializeBuilding(string path)
    {
        GameManager.ins.Building = Building.DeserializeFromXML(path);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            QuickLoad();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            QuickSave();
        }
    }

    public void QuickSave()
    {
        string dataPath = Application.persistentDataPath;

        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }

        // attempt to save here data
        try
        {
            // save datahere
            string filePath = dataPath + @"/Hsh-quick-save.xml";
            Debug.Log("Save data to: " + filePath);
            SerializeBuilding(filePath);
        }
        catch (Exception e)
        {
            
            Debug.LogError("Failed to save data to: " + dataPath);
            Debug.LogError("Error " + e.Message);
        }
    }

    public void QuickLoad()
    {
        string dataPath = Application.persistentDataPath;

        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }

        try
        {
            // load datahere
            string filePath = dataPath + @"/Hsh-quick-save.xml";
            Debug.Log("Save data to: " + filePath);
            DeserializeBuilding(filePath);
            Drawing2DController.ins.RedrawCurrentStorey();
        }
        catch (Exception e)
        {

            Debug.LogError("Failed to load data from: " + dataPath);
            Debug.LogError("Error " + e.Message);
        }
    }
}
