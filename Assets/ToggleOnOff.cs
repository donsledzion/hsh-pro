using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class ToggleOnOff : MonoBehaviour, IPointerDownHandler
{

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;

    [SerializeField]
    UnityEvent onToggle;

    [SerializeField]
    GameObject equipmentSelection;

    [SerializeField] GameObject selectButton;

    [SerializeField]
    public Image backgroundImage;

    [SerializeField]
    private bool _isOn = false;

    private bool _gameObjectIsOn;

    public bool isOn
    {
        get
        {
            return _isOn;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);
    }

    public void Toggle(bool value)
    {
        if (value != isOn)
        {
            _isOn = value;

            ToggleColor(isOn);

            ToggleEquipmentSelection(isOn);

        }

        if (valueChanged != null)
        {
            valueChanged(isOn);
        }

        onToggle?.Invoke();
    }
    public void ToggleColor(bool value)
    {
        if (value)
            backgroundImage.enabled = true;
        else
            backgroundImage.enabled = false;
    }

    public void ToggleObject()
    {
        if (_gameObjectIsOn)
        {
            _gameObjectIsOn = false;
        }
        else
        {
            _gameObjectIsOn = true;
        }
    }

    public void ToggleEquipmentSelection(bool value)
    {
        if (value)
            equipmentSelection.SetActive(true);
        else
            equipmentSelection.SetActive(false);
        ToggleColor(value);
        _isOn = false;
    }



}
