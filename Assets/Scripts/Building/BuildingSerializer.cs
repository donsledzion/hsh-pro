using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Walls2D;

public class BuildingSerializer : MonoBehaviour
{
    public static BuildingSerializer ins { get; private set; }

    [ContextMenu("Serialize Building")]
    void SerializeBuilding(string path)
    {
        UpdateEquipmentLocation();
        GameManager.ins.Building.SerializeToXML(path);
    }
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

    void DeserializeBuilding(string path)
    {
        GameManager.ins.Building = Building.DeserializeFromXML(path);
        GameManager.ins.Building.RegenerateSectionsReferences();
        NewSheetCreator.Instance.CreateNewSheet(GameManager.ins.Building);
        ReferenceController.ins.StoreySwitcherDropdown.UpdateDropdown();
        Drawing2DController.ins.RegenerateBuildingDrawing();
    }
    void QuickSave()
    {
        SaveToFile("Hsh-quick-save.xml");
    }

    void QuickLoad()
    {
        ReferenceController.ins.ModeController.Mode2D();
        LoadFromFile("Hsh-quick-save.xml");
    }

    public void SaveToFile(string fileName)
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
            string filePath = Path.Combine(dataPath,fileName);
            SerializeBuilding(filePath);
            Debug.Log("Saved data to: " + filePath);
        }
        catch (Exception e)
        {

            Debug.LogError("Failed to save data to: " + dataPath);
            Debug.LogError("Error " + e.Message);
        }
    }

    public void LoadFromFile(string fileName)
    {
        string dataPath = Application.persistentDataPath;

        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }
        /*try
        {*/
            string filePath = Path.Combine(dataPath, fileName);
            Debug.Log("Load data from: " + filePath);
            DeserializeBuilding(filePath);/*
        }
        catch (Exception e)
        {

            Debug.LogError("Failed to load data from: " + dataPath);
            Debug.LogError("Error " + e.Message);
        }*/
    }

    private int UpdateEquipmentLocation()
    {
        List<EquipmentItem> equipmentItems = new List<EquipmentItem>(FindObjectsOfType<EquipmentItem>());
        Debug.Log("Found " + equipmentItems.Count + " items (inside UpdateEquipmentLocation() method)");
        foreach (EquipmentItem item in equipmentItems)
            item.UpdatePosition();

        return equipmentItems.Count;
    }

    public SaveFileData GetFileData(string fileName)
    {
        string dataPath = Application.persistentDataPath;

        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }
        try
        {
            string filePath = Path.Combine(dataPath, fileName);
            if (!File.Exists(filePath)) return new SaveFileData(null, null,null,"File doesn't exists.");
            if (!ValidSaveFile()) return new SaveFileData(null,null,null,"This is'n correct project file.") ;
            Building building = Building.DeserializeFromXML(filePath);
            FileInfo fileInfo = new FileInfo(filePath);
            SaveFileData data = new SaveFileData(
                fileName,
                building.Name,
                fileInfo.LastWriteTime.ToShortDateString() + ", " + fileInfo.LastWriteTime.ToShortTimeString(),
                null);
            return data;
        }
        catch
        {
            Debug.LogError("Couldn't read data!");
            return null;
        }
    }

    private bool ValidSaveFile()
    {
        //TODO - check if file contains valid project info
        return true;
    }
}

public class SaveFileData
{
    public string FileName;
    public string BuildingName;
    public string LastModificationTime;
    public string Message;

    public SaveFileData(string fileName, string buildingName, string lastModificationTime, string message)
    {
        FileName = fileName;
        BuildingName = buildingName;
        LastModificationTime = lastModificationTime;
        Message = message;
    }
}
