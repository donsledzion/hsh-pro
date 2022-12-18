using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingProjectController : MonoBehaviour
{
    string _selectedSlotName => _selectedSlot.gameObject.name + ".xml";
    ProjectSlotButton _selectedSlot;
    [SerializeField] UnityEvent _afterAssignEvent;

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

    public void AsssignToLoadOnStart()
    {
        if (string.IsNullOrEmpty(_selectedSlotName)) return;
        LoadFileOnStart.fileToLoadOnStart = _selectedSlotName;
        _afterAssignEvent?.Invoke();
    }
}
