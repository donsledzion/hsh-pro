using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValidateInput : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    public void Validate()
    {
        if(input != null)
        {
            float value = float.Parse(input.text);
            if (value < 0) input.text = Mathf.Abs(value).ToString();
        }
    }
}
