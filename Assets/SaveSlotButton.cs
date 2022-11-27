using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlotButton : MonoBehaviour
{
    TextMeshProUGUI slotText;

    void Start()
    {
        slotText = GetComponentInChildren<TextMeshProUGUI>();    
        SavingProjectController savingController = GetComponentInParent<SavingProjectController>();
        GetComponent<Button>().onClick.AddListener(() => savingController.SelectMe(this.gameObject));
        SaveFileData fileData = BuildingSerializer.ins.GetFileData(gameObject.name + ".xml");
        if (string.IsNullOrEmpty(fileData.FileName))
        {
            Debug.LogWarning(fileData.Message);
            slotText.text = "---Pusty---";
        }
        else slotText.text = fileData.BuildingName +  " - " + fileData.LastModificationTime;
    }
}
