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
    [SerializeField] RectTransform _whiteboardBackground;
    [SerializeField] GridController _gridController;
    CanvasController _canvasController => CanvasController.ins;


    public void CreateNewSheet(int width, int height)
    {
        _gridController.ClearGrid();
        _canvasController.ClearPointsLabels();
        _whiteboardBackground.sizeDelta = new Vector2(width, height);
        _canvasController.ResetCanvas();
        Drawing2DController.ins.AdjustCanvasPosition();
        _backGroundWhiteboard.SetActive(true); // Daniel dopisa³ w³¹cz wy³¹cz background.
        modalController?.Hide();
    }

    public void CreateNewSheet(Vector2 newSize)
    {
        CreateNewSheet((int)newSize.x, (int)newSize.y);
    }

    public void NewSheet()
    {
        int width;
        int height;
        int.TryParse(_inputWidth.text, out width);
        int.TryParse(_inputHeight.text, out height);
        CreateNewSheet(width,height);
    }
}
