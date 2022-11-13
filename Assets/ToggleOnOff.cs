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

    [SerializeField]
    private Image backgroundImage;

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

    private void Toggle(bool value)
    {
        if (value != isOn)
        {
            _isOn = value;

            ToggleColor(isOn);

            //ToggleObject(isOn);

        }

        if (valueChanged != null)
        {
            valueChanged(isOn);
        }

        onToggle?.Invoke();
    }
    private void ToggleColor(bool value)
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

    private void ToggleObject(bool value)
    {
        if (value)
            equipmentSelection.SetActive(true);
        else
            equipmentSelection.SetActive(false);
    }

}
