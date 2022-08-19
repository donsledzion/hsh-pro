using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreyListButton : MonoBehaviour
{
    public Storey2D Storey2D { get; private set; }
    public bool IsChosen { get; set; }
    [SerializeField] Text buttonLabel;

    public void Initialize(Storey2D storey2D)
    {
        buttonLabel.text = storey2D.StoreyReference.Name;
        Storey2D = storey2D;
        IsChosen = false;
    }

    public void SwitchToStorey()
    {
        Drawing2DController.ins.SwitchToStorey(Storey2D.StoreyReference);
    }
}
