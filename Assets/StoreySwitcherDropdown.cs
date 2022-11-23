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
        _dropdown.value = CurrentStoreyIndex();


    }

    public static int CurrentStoreyIndex()
    {
        for (int i = 0; i < GameManager.ins.Building.Storeys.Count; i++)
            if (GameManager.ins.Building.Storeys[i] == GameManager.ins.Building.CurrentStorey) return i;
        return -1;
    }

    public void SwitchToSelectedStorey()
    {
        GameManager.ins.Building.SetCurrentStorey(GameManager.ins.Building.Storeys[_dropdown.value]);
        Drawing2DController.ins.SwitchToStorey(GameManager.ins.Building.CurrentStorey);

         
    }

}
