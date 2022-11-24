using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewSheetPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputBuildingName;
    [SerializeField] TMP_InputField _inputSheetWidth;
    [SerializeField] TMP_InputField _inputSheetHeight;

    public void CreateNewSheet()
    {
        NewSheetCreator.Instance.CreateNewSheet(int.Parse(_inputSheetWidth.text), int.Parse(_inputSheetHeight.text));
        GameManager.ins.Building = null;
        BuildingCreator.Instance.CreateNewBuilding();
    }
}
