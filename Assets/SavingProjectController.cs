using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SavingProjectController : MonoBehaviour
{
    string _selectedSlot;

    public void SaveToFile(string slotName)
    {
        BuildingSerializer.ins.SaveToFile(slotName);
    }

    public void SelectMe(GameObject go)
    {
        _selectedSlot = go.name + ".xml";
        Debug.Log("Clicked by: "+ go.name);
    }

    public void SaveSelected()
    {
        if(!string.IsNullOrEmpty(_selectedSlot))
            SaveToFile(_selectedSlot);
    }

    public void DeleteSelected()
    {
        //TODO: implement deleting slot
    }
}
