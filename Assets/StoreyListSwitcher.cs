using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreyListSwitcher : MonoBehaviour
{
    List<StoreyListButton> _storeyListButtons = new List<StoreyListButton>();


    [ContextMenu("Update List")]
    void UpdateList()
    {
        List<Storey2D> storeys2D = Drawing2DController.ins.Storeys2D;
        foreach(Storey2D storey2D in storeys2D)
        {

        }
    }
}
