using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine.UI;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private DoorAssetController invertoryItems;
    [SerializeField] private WindowAssetController invertoryItems2;

    private GameObject itemPrefab = null;

    [SerializeField]
    private TextMeshProUGUI description;

    private void Awake()
    {

        //invertoryItems.OnItemChoosen += PrefabToFit;
        invertoryItems.OnItemSelected += invertoryItems_OnItemSelectedDoor;
        invertoryItems2.OnItemSelected += invertoryItems_OnItemSelectedWindow;
    }

    private void PrefabToFit(object sender, ScriptableObjectsController itemSO)
    {
        Debug.Log(itemSO.prefab.name);
        CurrentPrefabController.ins.Door3DSelector.DoorPrefab = itemSO.prefab;
    }

    public void invertoryItems_OnItemSelectedWindow(object sender, ScriptableObjectsController itemSO)
    {
        Debug.Log(itemSO);
        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9900, 10000), Quaternion.identity);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
    }

    public void invertoryItems_OnItemSelectedDoor(object sender, ScriptableObjectsController itemSO)
    {
        Debug.Log(itemSO);
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
        itemPrefab.transform.eulerAngles += new Vector3(0,-eventData.delta.x, 0);
    }
}
