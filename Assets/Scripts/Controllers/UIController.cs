using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Grid ================================================

    [SerializeField] TextMeshProUGUI _gridSnapButtonLabel;

    [SerializeField] Slider gridLabelSlider;
    [SerializeField] TextMeshProUGUI gridLabelText;
    [SerializeField] TextMeshProUGUI _angleSnapSliderLabel;


    [SerializeField] TextMeshProUGUI _dynamicDimensionsButtonLabel;

    [SerializeField] Slider _angleSnapSlider;
    [SerializeField] TextMeshProUGUI _angleSnapButtonLabel;

    public void UpdateGridSnapToggleButton()
    {
        if (GameManager.ins.GridSnap)
            _gridSnapButtonLabel.text = "Grid snap: ON";
        else
            _gridSnapButtonLabel.text = "Grid snap: OFF";
    }

    public void UpdateGridSizeLabel()
    {

        gridLabelText.text = "Grid density: " + gridLabelSlider.value + " cm";
    }

    public Vector3 GridSnap(Vector3 pointerPosition)
    {
        float x = GameManager.ins.DrawingCanvasBackgroundLBCorner.x
                + Mathf.Round((pointerPosition.x - GameManager.ins.DrawingCanvasBackgroundLBCorner.x)
                / (gridLabelSlider.value * GameManager.ins.Zoom))
                * (gridLabelSlider.value * GameManager.ins.Zoom);

        float y = GameManager.ins.DrawingCanvasBackgroundLBCorner.y
                + Mathf.Round((pointerPosition.y - GameManager.ins.DrawingCanvasBackgroundLBCorner.y)
                / (gridLabelSlider.value * GameManager.ins.Zoom))
                * (gridLabelSlider.value * GameManager.ins.Zoom);

        return new Vector3(x, y, 0);
    }

    public void UpdateAngleSnap()
    {
        if (GameManager.ins.AngleSnap)
            _angleSnapButtonLabel.text = "Angle snap: ON";
        else
            _angleSnapButtonLabel.text = "Angle snap: OFF";
    }

    public void UpdateAngleSnapLabel()
    {
        _angleSnapSliderLabel.text = "Snap angle: " + _angleSnapSlider.value + "°";
    }

    public void UpdateDynamicDimensions()
    {
        if(GameManager.ins.DynamicDimensions)
            _dynamicDimensionsButtonLabel.text = "Dynamic Dimesions\nON";
        else
            _dynamicDimensionsButtonLabel.text = "Dynamic Dimesions\nOFF";
    }

    public void ToggleGridSnap()
    {
        GameManager.ins.ToggleGridSnap();
    }
    
    public void ToggleAngleSnap()
    {
        GameManager.ins.ToggleAngleSnap();
    }

}
