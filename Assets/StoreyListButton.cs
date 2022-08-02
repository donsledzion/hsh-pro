using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreyListButton : MonoBehaviour
{
    public Storey2D Storey2D { get; private set; }
    public bool IsChosen { get; set; }
    [SerializeField] TextMeshProUGUI buttonLabel;

    public void Initialize(Storey2D storey2D)
    {
        buttonLabel.text = storey2D.StoreyReference.Name;
        Storey2D = storey2D;
        IsChosen = false;
    }
}
