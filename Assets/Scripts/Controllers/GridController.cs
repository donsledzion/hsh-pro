using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridController : MonoBehaviour
{
    [SerializeField] UIController uIController;
    [SerializeField] Slider gridLabelSlider;
    [SerializeField] TextMeshProUGUI gridToggleButtonText;



    [SerializeField] GameObject whiteboard;
    [SerializeField] GameObject gridDotPrefab;
    [SerializeField] Transform dotsContainer;

    private bool _gridIsOn;
    public void UpdateLabel()
    {
        uIController.UpdateGridSizeLabel();
        if (_gridIsOn)
            GenerateGrid();
    }

    private void GenerateGrid()
    {
        _gridIsOn = true;
        ClearGrid();
        int gridOffset = (int)gridLabelSlider.value;
        Vector2 gridSize = new Vector2(whiteboard.GetComponent<RectTransform>().rect.width, whiteboard.GetComponent<RectTransform>().rect.height);
        for(int i  = gridOffset; i < gridSize.y; i+=gridOffset)
            for (int j = gridOffset; j < gridSize.x; j += gridOffset)
            {
                GameObject dot = Instantiate(gridDotPrefab, GameManager.ins.DrawingCanvasBackgroundLBCorner+new Vector3(j * GameManager.ins.Zoom, i * GameManager.ins.Zoom, 0),gridDotPrefab.transform.rotation);
                dot.transform.SetParent(dotsContainer);
            }
    }

    public void ClearGrid()
    {
        foreach(Transform dot in dotsContainer.GetComponentsInChildren<Transform>())
        {
            if(dot!=dotsContainer)
                Destroy(dot.gameObject);
        }
    }

    public void ToggleGrid()
    {
        if(_gridIsOn)
        {
            _gridIsOn = false;
            ClearGrid();
            gridToggleButtonText.text = "Grid: OFF";
        }
        else
        {
            _gridIsOn = true;
            GenerateGrid();
            gridToggleButtonText.text = "Grid: ON";
        }
    }
}
