using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateSlider : MonoBehaviour
{
    [SerializeField] Slider _gridDensitySlider;
    [SerializeField] TMP_InputField _deinsityInputField;

    public void UpdateSliderValue()
    {
        _gridDensitySlider.value = Mathf.Clamp(float.Parse(_deinsityInputField.text),1f,100f);
    }
}
