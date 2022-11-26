using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SheetResizer : MonoBehaviour
{
    WhiteboardBackgroundInfo _sheet => ReferenceController.ins.WhiteboardBackgroundInfo;
    [SerializeField] TMP_InputField _newWidth;
    [SerializeField] TMP_InputField _newHeight;
    [SerializeField] TMP_InputField _sheetName;
    [SerializeField] TextMeshProUGUI _validationErrorDisplay;


    private void OnEnable()
    {
        _sheetName.text = GameManager.ins.Building.Name;
    }

    public void ResizeSheet()
    {
        Vector2 newSize = new Vector2(int.Parse(_newWidth.text), int.Parse(_newHeight.text));
        _sheet.MyRect.sizeDelta= newSize;
        Drawing2DController.ins.AdjustCanvasPosition();
        CanvasController.ins.ResetCanvas();

    }

    private bool ValidateNewSize()
    {
        return true;
    }
}
