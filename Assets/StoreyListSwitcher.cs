using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreyListSwitcher : MonoBehaviour
{
    List<StoreyListButton> _storeyListButtons = new List<StoreyListButton>();
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonsContainer;


    [ContextMenu("Update List")]
    public void UpdateList()
    {
        List<Storey2D> storeys2D = Drawing2DController.ins.Storeys2D;
        Debug.Log("Updating storeys list: " + storeys2D.Count);
        ClearList();
        foreach(Storey2D storey2D in storeys2D)
        {
            GameObject button = Instantiate(buttonPrefab, buttonsContainer);
            StoreyListButton listButton = button.GetComponent<StoreyListButton>();
            listButton.Initialize(storey2D);
            _storeyListButtons.Add(listButton);
        }
    }

    void ClearList()
    {
        List<Storey2D> storeys2D = Drawing2DController.ins.Storeys2D;
        Debug.Log("Clearing storeys list: " + storeys2D.Count);
        foreach (StoreyListButton listButton in _storeyListButtons)
        {
            Destroy(listButton.gameObject);
        }
        _storeyListButtons.Clear();
    }
}
