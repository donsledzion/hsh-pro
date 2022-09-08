using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private DoorAssetController invertoryItems;
    private GameObject itemPrefab = null;

    private void Awake()
    {
        invertoryItems.OnItemSelected += invertoryItems_OnItemSelected;
    }

    public void invertoryItems_OnItemSelected(object sender, DoorScriptableObjects itemSO)
    {

        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000), Quaternion.identity);
    }

    public void destroyPrefab() {


        if(itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);
        }
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        itemPrefab.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }
}
