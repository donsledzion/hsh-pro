using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class ToggleOnOff : MonoBehaviour
{

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;

    [SerializeField]
    UnityEvent onToggle;

    [SerializeField]
    private bool _isOn = false;
    public bool isOn
    {
        get
        {
            return _isOn;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("hejehejeh");
        Toggle(!isOn);
    }

    private void Toggle(bool value)
    {
        if (value != isOn)
        {
            _isOn = value;

            ToggleColor(isOn);

        }

        if (valueChanged != null)
        {
            valueChanged(isOn);
        }

        onToggle?.Invoke();
    }
    private void ToggleColor(bool value)
    {
       /* if (value)
            backgroundImage.DOColor(onColor, tweenTime);
        else
            backgroundImage.DOColor(offColor, tweenTime);*/
    }

}
