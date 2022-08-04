using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreyInfoDisplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI _label;
    [SerializeField]TextMeshProUGUI _number;
    [SerializeField] TextMeshProUGUI _elevation;
    [SerializeField] TextMeshProUGUI _height;    

    [ContextMenu("UpdateCanvasInfo")]
    public void UpdateCanvasInfo()
    {
        _label.text = "Storey(" + (GameManager.ins.Building.CurrentStorey.Number+1) + "/" + GameManager.ins.Building.Storeys.Count + "):";
        _number.text = "No.:" + GameManager.ins.Building.CurrentStorey.Number.ToString();
        _elevation.text = "Elevation:" + GameManager.ins.Building.CurrentStorey.Elevation.ToString();
        _height.text = "Height:" + GameManager.ins.Building.CurrentStorey.Height.ToString();
    }
}
