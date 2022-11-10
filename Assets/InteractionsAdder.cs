using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class InteractionsAdder : MonoBehaviour
{
    [SerializeField] List<EquipmentItem> equipmentItems = new List<EquipmentItem>();

    [ContextMenu("CollectAllItems")]
    void CollectAllItems()
    {
        equipmentItems = new List<EquipmentItem>(FindObjectsOfType<EquipmentItem>());
    }

    [ContextMenu("MakeItemsInteractable")]
    void MakeItemsInteractable()
    {
        foreach(EquipmentItem item in equipmentItems)
        {
            /*item.gameObject.AddComponent<Interactable>();
            item.gameObject.AddComponent<Throwable>();*/
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            //rb.mass = 500;
        }            
    }
}
