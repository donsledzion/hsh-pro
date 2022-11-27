using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SavingProjectController : MonoBehaviour
{
    string _selectedSlotName => _selectedSlot.gameObject.name + ".xml";
    ProjectSlotButton _selectedSlot;


    public void SaveToFile(string slotName)
    {
        BuildingSerializer.ins.SaveToFile(slotName);
    }

    public void SelectMe(GameObject go)
    {
        _selectedSlot = go.GetComponent<ProjectSlotButton>();
        Debug.Log("Clicked by: "+ go.name);
    }

    public void SaveSelected()
    {
        if(!string.IsNullOrEmpty(_selectedSlotName))
        {
            SaveToFile(_selectedSlotName);
            _selectedSlot.UpdateName();
        }
    }

    public void DeleteSelected()
    {
        //TODO: implement deleting slot
    }
}
