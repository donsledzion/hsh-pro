using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PointLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI labelText;

    public void SetLabelText(string text)
    {
        labelText.text = text;
    }
}
