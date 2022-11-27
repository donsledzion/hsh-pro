using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewSheetPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputBuildingName;
    [SerializeField] TMP_InputField _inputSheetWidth;
    [SerializeField] TMP_InputField _inputSheetHeight;

    /*public void CreateNewSheet()
    {
        NewSheetCreator.Instance.CreateNewSheet(int.Parse(_inputSheetWidth.text), int.Parse(_inputSheetHeight.text), _inputBuildingName.text);
        GameManager.ins.Building = null;
        Building building = BuildingCreator.Instance.CreateNewBuilding(_inputBuildingName.text);
        if(building != null)
        {
            Debug.Log("Building is not null!");
            building.Name = _inputBuildingName.text;
        }
        else
            Debug.Log("Building IS NULL!");
    }*/
}
