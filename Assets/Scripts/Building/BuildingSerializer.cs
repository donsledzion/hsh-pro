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
        Debug.Log("Updating Equipment locations...");
        int updatedItems = UpdateEquipmentLocation();
        Debug.Log("...done! Updated for " + updatedItems + " items.");
        GameManager.ins.Building.SerializeToXML(path);
    }

    void DeserializeBuilding(string path)
    {
        GameManager.ins.Building = Building.DeserializeFromXML(path);
        ReferenceController.ins.StoreySwitcherDropdown.UpdateDropdown();
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
            Debug.Log("Save data to: " + filePath);
            DeserializeBuilding(filePath);
            Drawing2DController.ins.RegenerateBuildingDrawing();
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
