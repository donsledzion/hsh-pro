using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProjectSlotButton : MonoBehaviour
{
    protected TextMeshProUGUI slotText;
    public string Name {get { return slotText.text; } }

    protected virtual void Start()
    {
        UpdateName();
    }

    public void UpdateName()
    {
        SaveFileData fileData = BuildingSerializer.ins.GetFileData(gameObject.name + ".xml");
        if (string.IsNullOrEmpty(fileData.FileName))
        {
            Debug.LogWarning(fileData.Message);
            slotText.text = "---Pusty---";
        }
        else slotText.text = fileData.BuildingName +  " - " + fileData.LastModificationTime;
    }
}
