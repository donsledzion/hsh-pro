using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AssetController : MonoBehaviour
{
    public System.EventHandler<ScriptableObjectsController> OnItemSelected;
    public System.EventHandler<ScriptableObjectsController> OnItemChoosen;


    private List<ScriptableObjectsController> itemList = new List<ScriptableObjectsController>();
    private AssetBundle ItemAsset = null;
    [SerializeField] GameObject templateItemPrefab;
    GameObject inspectionCamera;
    GameObject item3DViewer;
    GameObject itemsGallery;
    EquipmentInsertionMode prefabToFitController;
    TMP_InputField inputField;
    [SerializeField] List<string> assetNamesList = new List<string>();

    private void Awake()
    {
        inspectionCamera = ReferenceController.ins.ItemInspectionCamera.gameObject;
        prefabToFitController = ReferenceController.ins.EquipmentInsertionMode;
        itemsGallery = ReferenceController.ins.GalleryOfItems.gameObject;
        item3DViewer = ReferenceController.ins.Item3DViewer.gameObject;
        inputField = ReferenceController.ins.GallerySearchField.GetComponent<TMP_InputField>();
        Init();
    }

    private void Init()
    {
        FetchItems();
        InstantiateList();
        inputField.onValueChanged.AddListener(OnInputValueChange);
    }

    private void InstantiateList()
    {
        foreach (ScriptableObjectsController item in itemList)
        {
            SetupInstance(item);
        }
    }

    private void SetupInstance(ScriptableObjectsController item)
    {
        GameObject g;
        g = Instantiate(templateItemPrefab, transform);
        g.name = item.name;
        GalleryItemTemplate template = g.GetComponent<GalleryItemTemplate>();
        template.Thumbnail.sprite = item.imagePreview;
        template.LayoutElement.preferredHeight = 300;
        template.ItemNameTMPro.text = item.name;
        template.SelectButton.onClick.AddListener(() =>
        {
            ItemPrefabToFit(item);

        });
        template.PreviewButton.onClick.AddListener(() =>
        {
            inspectionCamera.SetActive(true);
            GetItemPrefab(item);
        });
    }

    private void ItemPrefabToFit(ScriptableObjectsController item)
    {
        prefabToFitController.SwapPrefab(item.prefab);
        prefabToFitController.gameObject.SetActive(true);
        
        foreach(string assetName in assetNamesList)
        {
            string itemName = AssetBundleHelper.ExtractName(item);
            ItemAsset = AssetBundleLoader.ins.GetBundleLoadStatusByName(assetName)?.Bundle;
            if(ItemAsset!= null)
            {
                ScriptableObjectsController itemMatch = ItemAsset.LoadAsset(itemName) as ScriptableObjectsController;
                if(itemMatch!= null)
                    AssignNameAndBundleNames(itemName,assetName);
            }

        }
        
        item3DViewer.SetActive(false);
        itemsGallery.SetActive(false);
    }

    private void FetchItems()
    {
        foreach (string assetName in assetNamesList)
        {
            ItemAsset = AssetBundleLoader.ins.GetBundleLoadStatusByName(assetName)?.Bundle;
            
            if (ItemAsset) Debug.Log("Loaded successfuly " + assetName);
            else Debug.Log("Failed to load " + assetName);

            itemList.AddRange(ItemAsset.LoadAllAssets<ScriptableObjectsController>());
        }        
    }

    private void OnInputValueChange(string arg0)
    {
        if (!itemList.Exists(i => i.name == arg0))
        {
            DestroyAllPrefabs();
            Debug.Log("name do not exist");
            InstantiateList();
        }
        else
        {
            Debug.Log("I have found a name");
            FindItemPrefab(itemList.Find(i => i.name == arg0));
        }
    }

    public void GetItemPrefab(ScriptableObjectsController item)
    {
        item3DViewer.SetActive(true);
        itemsGallery.SetActive(false);
        OnItemSelected?.Invoke(this, item);
    }

    private void DestroyAllPrefabs()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void FindItemPrefab(ScriptableObjectsController item)
    {
        DestroyAllPrefabs();
        SetupInstance(item);
    }

    void AssignNameAndBundleNames(string item, string bundle)
    {
        prefabToFitController.AssetName = item;
        prefabToFitController.BundleName = bundle;
    }
}
