using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreySwitcherDropdown : MonoBehaviour
{
    [SerializeField] Dropdown _dropdown;

    [ContextMenu("Update Dropdown")]
    public void UpdateDropdown()
    {
        _dropdown.options.Clear();
        foreach (Storey storey in GameManager.ins.Building.Storeys)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = storey.Name;
            _dropdown.options.Add(option);
        }
        _dropdown.RefreshShownValue();
    }

    public void SwitchToSelectedStorey()
    {
        Drawing2DController.ins.SwitchToStorey(GameManager.ins.Building.Storeys[_dropdown.value]);
    }
}
