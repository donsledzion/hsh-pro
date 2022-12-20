using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProjectSlotButton : MonoBehaviour
{
    [SerializeField] bool _loadFromStreamingAssets = false;
    protected TextMeshProUGUI slotText;
    public string Name {get { return slotText.text; } }

    protected virtual void Start()
    {
        UpdateName(_loadFromStreamingAssets);
    }

    public void UpdateName(bool loadFromStreamingAssets = false)
    {
        SaveFileData fileData = BuildingSerializer.GetFileData(gameObject.name + ".xml",loadFromStreamingAssets);
        if (string.IsNullOrEmpty(fileData.FileName))
        {
            Debug.LogWarning(fileData.Message);
            slotText.text = "---Pusty---";
        }
        else slotText.text = fileData.BuildingName +  " - " + fileData.LastModificationTime;
    }
}
