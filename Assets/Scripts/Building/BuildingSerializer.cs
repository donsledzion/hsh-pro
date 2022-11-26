using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
        NewSheetCreator.Instance.CreateNewSheet(GameManager.ins.Building.SheetSize);
        ReferenceController.ins.StoreySwitcherDropdown.UpdateDropdown();
        Drawing2DController.ins.RegenerateBuildingDrawing();
    }
    void QuickSave()
    {
        SaveToFile("Hsh-quick-save.xml");
    }

    void QuickLoad()
    {
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
        try
        {
            string filePath = Path.Combine(dataPath, fileName);
            Debug.Log("Load data from: " + filePath);
            DeserializeBuilding(filePath);
        }
        catch (Exception e)
        {

            Debug.LogError("Failed to load data from: " + dataPath);
            Debug.LogError("Error " + e.Message);
        }
    }

    private int UpdateEquipmentLocation()
    {
        List<EquipmentItem> equipmentItems = new List<EquipmentItem>(FindObjectsOfType<EquipmentItem>());
        Debug.Log("Found " + equipmentItems.Count + " items (inside UpdateEquipmentLocation() method)");
        foreach (EquipmentItem item in equipmentItems)
            item.UpdatePosition();

        return equipmentItems.Count;
    }
}
