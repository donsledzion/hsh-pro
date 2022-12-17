using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AngleSnapValuesUpdater : MonoBehaviour
{
    [SerializeField] TMP_InputField _angleSnapInput;
    [SerializeField] Slider _angleSnapSlider;

    public void UpdateSliderValue()
    {
        _angleSnapSlider.value = Mathf.Clamp(float.Parse(_angleSnapInput.text),0f,90f);
    }

    public void UpdateInputValue()
    {
        Debug.Log("Slider value to insert into input: " + _angleSnapSlider.value.ToString());
        _angleSnapInput.text = _angleSnapSlider.value.ToString();
    }
}
