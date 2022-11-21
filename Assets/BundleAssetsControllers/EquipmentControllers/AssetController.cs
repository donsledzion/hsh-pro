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
    private AssetBundle furnitureAsset = null;
    [SerializeField] GameObject templateFurniturePrefab;
    GameObject inspectionCamera;
    GameObject item3DViewer;
    GameObject itemsGallery;
    EquipmentInsertionMode prefabToFitController;
    TMP_InputField inputField;
    [SerializeField] string nameOffAssetToLoad;

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
        FetchFurniture();
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
        g = Instantiate(templateFurniturePrefab, transform);
        g.name = item.name;
        GalleryItemTemplate template = g.GetComponent<GalleryItemTemplate>();
        template.Thumbnail.sprite = item.imagePreview;
        template.LayoutElement.preferredHeight = 300;
        template.ItemNameTMPro.text = item.name;
        template.SelectButton.onClick.AddListener(() =>
        {
            FurniturePrefabToFit(item);

        });
        template.PreviewButton.onClick.AddListener(() =>
        {
            inspectionCamera.SetActive(true);
            GetFurniturePrefab(item);
        });
    }

    private void FurniturePrefabToFit(ScriptableObjectsController item)
    {
        prefabToFitController.SwapPrefab(item.prefab);
        prefabToFitController.gameObject.SetActive(true);
        AssignNameAndBundleNames(AssetBundleHelper.ExtractName(item), nameOffAssetToLoad);
        item3DViewer.SetActive(false);
        itemsGallery.SetActive(false);
    }

    private void FetchFurniture()
    {
        Debug.Log("Failed to load");

        furnitureAsset = AssetBundleLoader.ins.GetBundleLoadStatusByName(nameOffAssetToLoad)?.Bundle;

        if (furnitureAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        itemList.AddRange(furnitureAsset.LoadAllAssets<ScriptableObjectsController>());
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
            FindFurniturePrefab(itemList.Find(i => i.name == arg0));
        }
    }

    public void GetFurniturePrefab(ScriptableObjectsController item)
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

    public void FindFurniturePrefab(ScriptableObjectsController item)
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
