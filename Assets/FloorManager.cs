using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject _floorButtonPrefab;
    [SerializeField] Button _deleteButton;
    [SerializeField] Button _orderUpButton;
    [SerializeField] Button _orderDownButton;
    [SerializeField] Transform _buttonsContainer;
    FloorSection2D _selectedFloorSection;

    Storey currentStorey => GameManager.ins.Building.CurrentStorey;

    private void Start()
    {
        UpdateList();
    }

    [ContextMenu("Update List")]
    public void UpdateList()
    {
        currentStorey.Floors.Sort();
        ClearList();
        foreach(FloorSection2D section in currentStorey.Floors)
        {
            GameObject newFloorButton = Instantiate(_floorButtonPrefab, _buttonsContainer);
            newFloorButton.GetComponent<FloorListButton>().Setup(section);
        }
    }

    public void UnselectFloor()
    {
        _selectedFloorSection = null;
        Drawing2DController.ins.DrawSelectedFloor(_selectedFloorSection);
    }

    public void UpdateButtons()
    {
        UpdateDeleteButton();
        UpdateOrderButtons();
    }

    public void UpdateOrderButtons()
    {
        _orderDownButton.interactable = _selectedFloorSection != null;
        _orderUpButton.interactable = _selectedFloorSection != null;
    }

    public void UpdateDeleteButton()
    {
        _deleteButton.interactable = _selectedFloorSection != null;
    }

    public void DeleteSelectedSection()
    {
        currentStorey.RemoveFloorSection(_selectedFloorSection);
        
        Drawing2DController.ins.RedrawCurrentStorey();
        UnselectFloor();
        UpdateList();
    }

    public void SelectButton(FloorListButton button)
    {
        if (button.GetComponent<Toggle>().isOn)
            _selectedFloorSection = button.FloorSection;
        else
            _selectedFloorSection = null;
        UpdateButtons();
        Drawing2DController.ins.DrawSelectedFloor(_selectedFloorSection);
    }

    void ClearList()
    {
        _selectedFloorSection = null;
        foreach(Transform _buttonTransform in _buttonsContainer)
        {
            if(_buttonTransform.gameObject != _floorButtonPrefab)
            {
                Destroy(_buttonTransform.gameObject);
            }    
        }
    }

    public void IncreaseOrder()
    {
        _selectedFloorSection.IncreseOrder();
        _selectedFloorSection = null;
        UpdateButtons();
        UpdateList();
    }

    public void DecreseOrder()
    {
        _selectedFloorSection.DecreseOrder();
        UpdateList();
    }
}
