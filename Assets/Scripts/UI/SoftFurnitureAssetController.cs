using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoftFurnitureAssetController : MonoBehaviour
{

    public System.EventHandler<ScriptableObjectsController> OnItemSelected;
    public System.EventHandler<ScriptableObjectsController> OnItemChoosen;


    private List<ScriptableObjectsController> itemList = new List<ScriptableObjectsController>();
    private AssetBundle softFurnitureAsset;
    [SerializeField] GameObject templateSoftFurniturePrefab;
    [SerializeField] GameObject item3DViewer;
    [SerializeField] GameObject itemsGallery;
    GameObject itemPrefab;
    public TMP_InputField inputField;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        FetchSoftFurniture();
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
        g = Instantiate(templateSoftFurniturePrefab, transform);
        g.name = item.name;
        g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = item.imagePreview;
        g.transform.GetChild(0).GetChild(1).GetComponent<LayoutElement>().preferredHeight = 300;
        g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;

        g.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            SoftFurniturePrefabToFit(item);

        });

        g.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            GetSoftFurniturePrefab(item);
        });
    }

    private void SoftFurniturePrefabToFit(ScriptableObjectsController item)
    {
        //CurrentPrefabController.ins.Window3DSelector.ItemPrefab = item.prefab;
        item3DViewer.SetActive(false);
        itemsGallery.SetActive(false);
    }

    private void FetchSoftFurniture()
    {
        softFurnitureAsset = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/furniture_bundle");

        if (softFurnitureAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        itemList.AddRange(softFurnitureAsset.LoadAllAssets<ScriptableObjectsController>());
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

    public void GetSoftFurniturePrefab(ScriptableObjectsController item)
    {
        item3DViewer.SetActive(true);
        itemsGallery.SetActive(false);
        Debug.Log(item);
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
}


