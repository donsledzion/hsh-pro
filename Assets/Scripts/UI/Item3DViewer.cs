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
    [SerializeField] private SofaFurnitureController invertorySofaFurniture;
    [SerializeField] private CornersFurnitureController invertoryCornersFurniture;
    [SerializeField] private ArmchairFurnitureController invertoryArmchairFurniture;
    [SerializeField] private CouchiesAssetController invertoryCoachiesFurniture;
    [SerializeField] private Camera previewCamera;


    [SerializeField] private GameObject itemViewer;

    private GameObject itemPrefab = null;
    private GameObject furniturePrefab = null;

    private GameObject itemPrefabNotMovable = null;

    [SerializeField]
    private TextMeshProUGUI description;

    

    private void Awake()
    {
        invertoryDoor.OnItemSelected += invertoryItems_OnItemSelectedDoor;
        invertoryWindow.OnItemSelected += invertoryItems_OnItemSelectedWindow;
        invertoryFloor.OnItemSelected += invertoryItems_OnItemSelectedFloor;
        invertorySofaFurniture.OnItemSelected += invertoryItems_OnItemSelectedSofaFurniture;
        invertoryCornersFurniture.OnItemSelected += invertoryItems_OnItemSelectedCornersFurniture;
        invertoryArmchairFurniture.OnItemSelected += invertoryItems_OnItemSelectedArmchairsFurniture;
        invertoryCoachiesFurniture.OnItemSelected += invertoryItems_OnItemCoachiesSofaFurniture;
    }

    private void invertoryItems_OnItemCoachiesSofaFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        furniturePrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.6f, 10003), Quaternion.identity);
        furniturePrefab.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        previewCamera.orthographicSize = 1.5f;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemSelectedArmchairsFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        furniturePrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.6f, 10003), Quaternion.identity);
        furniturePrefab.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        previewCamera.orthographicSize = 1.5f;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemSelectedCornersFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        furniturePrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.6f, 10003), Quaternion.identity);
        furniturePrefab.transform.localScale = new Vector3(0.5f, 1, 0.8f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        previewCamera.orthographicSize = 1.5f;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }


    /*    private void PrefabToFit(object sender, ScriptableObjectsController itemSO)
        {
            Debug.Log(itemSO.prefab.name);
            CurrentPrefabController.ins.Door3DSelector.ItemPrefab = itemSO.prefab;
        }*/

    private void invertoryItems_OnItemSelectedSofaFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        furniturePrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.6f, 10003), Quaternion.identity);
        furniturePrefab.transform.localScale = new Vector3(0.5f, 1, 0.8f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        previewCamera.orthographicSize = 1.5f;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }


    public void invertoryItems_OnItemSelectedWindow(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000), Quaternion.identity);

        if (itemPrefab.GetComponent<BoxCollider>().size.x>2)
        {
            itemPrefab.transform.position = new Vector3(10000, 10000, 10001);
        }

        itemPrefab.transform.localScale = new Vector3(0.5f, 1, 0.5f);

        previewCamera.orthographicSize = 1f;

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

        itemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000.80f), Quaternion.identity);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        itemPrefab.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        previewCamera.orthographicSize = 1.2f;

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            CurrentPrefabController.ins.Door3DSelector.ItemPrefab = itemSO.prefab;
            itemViewer.SetActive(false);
            destroyPrefab();
        });
    }

    public void invertoryItems_OnItemSelectedFloor(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        itemPrefabNotMovable = Instantiate(itemSO.prefab, new Vector3(10000, 10000, 10000.80f), Quaternion.identity);
        //itemPrefab.transform.localScale = new Vector3(0.5f, 1, 0.5f);
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
        else if (furniturePrefab != null)
        {
            Destroy(furniturePrefab.gameObject);
        }


    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (itemPrefab != null)
        {
            itemPrefab.transform.eulerAngles += new Vector3(0, -eventData.delta.x, 0);
        }

        if (furniturePrefab != null)
        {
            furniturePrefab.transform.eulerAngles += new Vector3(0, -eventData.delta.x, 0); //new Vector3(eventData.delta.y, -eventData.delta.x);
        }

    }
}
