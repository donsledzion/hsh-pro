using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreyListSwitcher : MonoBehaviour
{
    List<StoreyListButton> _storeyListButtons = new List<StoreyListButton>();
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonsContainer;


    [ContextMenu("Update List")]
    void UpdateList()
    {
        List<Storey2D> storeys2D = Drawing2DController.ins.Storeys2D;
        foreach(Storey2D storey2D in storeys2D)
        {
            GameObject button = Instantiate(buttonPrefab, buttonsContainer);
            StoreyListButton listButton = button.GetComponent<StoreyListButton>();
            listButton.Initialize(storey2D);
        }
    }
}
