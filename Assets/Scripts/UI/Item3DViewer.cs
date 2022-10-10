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
    [SerializeField] private DoorAssetController invertoryDoor;
    [SerializeField] private WindowAssetController invertoryWindow;
    [SerializeField] private FloorAssetController invertoryFloor;

    [SerializeField] private GameObject itemViewer;

    private GameObject itemPrefab = null;
    private GameObject itemPrefabNotMovable = null;

    [SerializeField]
    private TextMeshProUGUI description;

    private void Awake()
    {
        
        //invertoryItems.OnItemChoosen += PrefabToFit;
        invertoryDoor.OnItemSelected += invertoryItems_OnItemSelectedDoor;
        invertoryWindow.OnItemSelected += invertoryItems_OnItemSelectedWindow;
        invertoryFloor.OnItemSelected += invertoryItems_OnItemSelectedFloor;
    }

    private void PrefabToFit(object sender, ScriptableObjectsController itemSO)
    {
        Debug.Log(itemSO.prefab.name);
        CurrentPrefabController.ins.Door3DSelector.ItemPrefab = itemSO.prefab;
    }

    public void invertoryItems_OnItemSelectedWindow(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        Debug.Log(itemSO);
        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000), Quaternion.identity);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            CurrentPrefabController.ins.Door3DSelector.ItemPrefab = itemSO.prefab;
            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    public void invertoryItems_OnItemSelectedDoor(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        Debug.Log(itemSO);
        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000.80f), Quaternion.identity);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            CurrentPrefabController.ins.Door3DSelector.ItemPrefab = itemSO.prefab;
            itemViewer.SetActive(false);
            destroyPrefab();
        });
    }

    public void invertoryItems_OnItemSelectedFloor(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        Debug.Log(itemSO);
        itemPrefabNotMovable = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000.80f), Quaternion.identity);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            
            itemViewer.SetActive(false);
            destroyPrefab();
        });
    }

    public void destroyPrefab() {


        if(itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);

        }else if(itemPrefabNotMovable!=null)
        {
            Destroy(itemPrefabNotMovable.gameObject);
        }

        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (itemPrefab != null)
        {
            itemPrefab.transform.eulerAngles += new Vector3(0, -eventData.delta.x, 0);
        }
    }
}
