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
    [SerializeField] private AssetController invertorySofaFurniture;
    [SerializeField] private AssetController invertoryCornersFurniture;
    [SerializeField] private AssetController invertoryArmchairFurniture;
    [SerializeField] private AssetController invertoryCoachiesFurniture;
    [SerializeField] private AssetController invertoryBedsFurniture;
    [SerializeField] private AssetController invertoryKitchenFurniture;
    [SerializeField] private AssetController invertoryLivingRoomFurniture;
    [SerializeField] private AssetController invertoryDiningRoomFurniture;
    [SerializeField] private AssetController invertoryBathroomItems;
    [SerializeField] private AssetController invertoryBedroomItems;
    [SerializeField] private AssetController invertoryLargeAGDItems;
    [SerializeField] private AssetController invertorySmallAGDItems;
    [SerializeField] private AssetController invertoryHiFiItems;
    [SerializeField] private AssetController invertoryRadioItems;
    [SerializeField] private AssetController invertoryCeilingLightsItems;
    [SerializeField] private AssetController invertoryFloorLightsItems;
    [SerializeField] private AssetController invertoryFreeStandingLightsItems;
    [SerializeField] private AssetController invertoryWallLightsItems;
    [SerializeField] private AssetController invertoryAccessoriesItems;
    [SerializeField] private GameObject surfaceSelector;
    Camera previewCamera;


    [SerializeField] private GameObject itemViewer;


    private GameObject itemPrefab = null;
    private GameObject ItemPrefab = null;

    private GameObject itemPrefabNotMovable = null;

    [SerializeField]
    private TextMeshProUGUI description;

    

    private void Awake()
    {
        invertoryDoor.OnItemSelected += invertoryItems_OnItemSelectedDoor;
        invertoryWindow.OnItemSelected += invertoryItems_OnItemSelectedWindow;
        invertoryFloor.OnItemSelected += invertoryItems_OnItemSelectedFloor;
        invertorySofaFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryCornersFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryArmchairFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryCoachiesFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryBedsFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryKitchenFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryLivingRoomFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryDiningRoomFurniture.OnItemSelected += invertoryItems_OnItemShow;
        invertoryBathroomItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryBedroomItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryLargeAGDItems.OnItemSelected += invertoryItems_OnItemShow;
        invertorySmallAGDItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryHiFiItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryRadioItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryCeilingLightsItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryFloorLightsItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryFreeStandingLightsItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryWallLightsItems.OnItemSelected += invertoryItems_OnItemShow;
        invertoryAccessoriesItems.OnItemSelected += invertoryItems_OnItemShow;
        previewCamera = ReferenceController.ins.ItemInspectionCamera.Camera;

    }

    private void invertoryItems_OnItemShow(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        Vector3 positionOfItem;

        
        switch (itemSO.prefab.tag)
        {

            case "TopLocker":
                positionOfItem = new Vector3(10000, 9998, 10003);
                break;

            case "Stove":
                positionOfItem = new Vector3(10000, 10000, 10003);
                break;            
            
            case "CeilingLight":
                positionOfItem = new Vector3(10000, 10000.5f, 10003);
                break;            
            
            case "WallLight":
                positionOfItem = new Vector3(10000, 9995, 10003);
                break;
                            
            case "WallAccessories":
                positionOfItem = new Vector3(10000, 9998.5f, 10003);
                break;

            default :  positionOfItem = new Vector3(10000, 9999.2f, 10003);
                break;

        }
        

        ItemPrefab = Instantiate(itemSO.prefab, positionOfItem, Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale;//new Vector3(0.5f, 1, 0.5f);
        if (ItemPrefab.tag == "Stove") ItemPrefab.transform.localRotation = Quaternion.Euler(-52,0,0) ;
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemKitchenFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();

        Vector3 positionOfItem;

        if (itemSO.prefab.tag == "TopLocker") positionOfItem = new Vector3(10000, 9998, 10003);
        else positionOfItem = new Vector3(10000, 9999.2f, 10003);

        ItemPrefab = Instantiate(itemSO.prefab, positionOfItem, Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale;//new Vector3(0.5f, 1, 0.5f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;
        

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemBedsFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        ItemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.2f, 10003), Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale;//new Vector3(0.5f, 1, 0.5f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;        

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemCoachiesFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        ItemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.2f, 10003), Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale;//new Vector3(0.5f, 1, 0.5f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;        

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemSelectedArmchairsFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        ItemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.2f, 10003), Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale; //new Vector3(0.5f, 1, 0.5f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;        

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            itemViewer.SetActive(false);
            destroyPrefab();

        });
    }

    private void invertoryItems_OnItemSelectedCornersFurniture(object sender, ScriptableObjectsController itemSO)
    {
        destroyPrefab();
        ItemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.6f, 10003), Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale; //new Vector3(0.5f, 1, 0.8f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;

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
        ItemPrefab = Instantiate(itemSO.prefab, new Vector3(10000, 9999.2f, 10003), Quaternion.identity);
        ItemPrefab.transform.localScale = ItemPrefab.GetComponent<EquipmentItem>().ThumbScale; //new Vector3(0.5f, 1, 0.8f);
        description.GetComponent<TextMeshProUGUI>().text = itemSO.Description.text;

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
            surfaceSelector.GetComponent<FittingModeSwitcher>().WindowsFittingMode();
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

        this.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

            CurrentPrefabController.ins.Door3DSelector.ItemPrefab = itemSO.prefab;
            surfaceSelector.GetComponent<FittingModeSwitcher>().DoorFittingMode();
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
        else if (ItemPrefab != null)
        {
            Destroy(ItemPrefab.gameObject);
        }


    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (itemPrefab != null)
        {
            itemPrefab.transform.eulerAngles += new Vector3(0, -eventData.delta.x, 0);
        }

        if (ItemPrefab != null)
        {
            ItemPrefab.transform.eulerAngles += new Vector3(0, -eventData.delta.x, 0);// new Vector3(eventData.delta.y, -eventData.delta.x);
        }

    }
}
