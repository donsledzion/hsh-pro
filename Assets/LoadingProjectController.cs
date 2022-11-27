using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingProjectController : MonoBehaviour
{
    string _selectedSlotName => _selectedSlot.gameObject.name + ".xml";
    ProjectSlotButton _selectedSlot;


    public void LoadFromFile(string slotName)
    {
        BuildingSerializer.ins.LoadFromFile(slotName);
    }
    public void SelectMe(GameObject go)
    {
        _selectedSlot = go.GetComponent<ProjectSlotButton>();
        Debug.Log("Clicked by: " + go.name);
    }
    public void LoadSelected()
    {
        if (!string.IsNullOrEmpty(_selectedSlotName))
        {
            LoadFromFile(_selectedSlotName);
            _selectedSlot.UpdateName();
        }
    }
}
