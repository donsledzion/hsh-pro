using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewSheetCreator: MonoBehaviour
{
    public static NewSheetCreator Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField]
    private GameObject _backGroundWhiteboard;

    [SerializeField]
    ModalWindowController modalController;

    [SerializeField] TMP_InputField _inputWidth;
    [SerializeField] TMP_InputField _inputHeight;
    [SerializeField] TMP_InputField _inputName;
    [SerializeField] RectTransform _whiteboardBackground;
    [SerializeField] GridController _gridController;
    CanvasController _canvasController => CanvasController.ins;


    public void CreateNewEmptySheet(int width, int height, string buildingName)
    {
        _gridController.ClearGrid();
        _canvasController.ClearPointsLabels();
        _whiteboardBackground.sizeDelta = new Vector2(width, height);
        Debug.Log("New sheet size: " + _whiteboardBackground.sizeDelta);
        _canvasController.ResetCanvas();
        Drawing2DController.ins.AdjustCanvasPosition();
        _backGroundWhiteboard.SetActive(true); // Daniel dopisa³ w³¹cz wy³¹cz background.
        modalController?.Hide();
        GameManager.ins.Building = null;
        ReferenceController.ins.BuildingDestroyer.DestroyBuilding();
        Building building = BuildingCreator.Instance.CreateNewBuilding(buildingName);
    }

    public void CreateNewSheetForBuilding(Building building)
    {
        _gridController.ClearGrid();
        _canvasController.ClearPointsLabels();
        _whiteboardBackground.sizeDelta = building.SheetSize;
        Debug.Log("New sheet size: " + _whiteboardBackground.sizeDelta);
        _canvasController.ResetCanvas();
        Drawing2DController.ins.AdjustCanvasPosition();
        _backGroundWhiteboard.SetActive(true); // Daniel dopisa³ w³¹cz wy³¹cz background.
        modalController?.Hide();
    }

    public void CreateNewSheet(Vector2 newSize, string buildingName)
    {
        CreateNewEmptySheet((int)newSize.x, (int)newSize.y, buildingName);
    }

    public void CreateNewSheet(Building building)
    {
        if (building != null)
        {
            CreateNewSheetForBuilding(building);
        }
        else
            Debug.LogWarning("Building is null! Something is wrong!!!");
    }

    public void NewSheet()
    {
        int width = int.Parse(_inputWidth.text);
        int height = int.Parse(_inputHeight.text);
        
        string buildingName;
        buildingName = _inputName.text;
        CreateNewEmptySheet(width,height, buildingName);
    }
}
