using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System.Text;
using UnityEditor;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private DoorAssetController invertoryItems;
    private GameObject itemPrefab = null;
    [SerializeField]
    private TextMeshProUGUI description;

    private void Awake()
    {
        invertoryItems.OnItemSelected += invertoryItems_OnItemSelected;
        Debug.Log("Ping");
    }

    public void invertoryItems_OnItemSelected(object sender, DoorScriptableObjects itemSO)
    {
        Debug.Log("Ping");
        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000), Quaternion.identity);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
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
