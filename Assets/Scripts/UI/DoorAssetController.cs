using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorAssetController : MonoBehaviour
{
    public System.EventHandler<ScriptableObjectsController> OnItemSelected;
    public System.EventHandler<ScriptableObjectsController> OnItemChoosen;


    private List<ScriptableObjectsController> itemList = new List<ScriptableObjectsController>();
    private AssetBundle doorAsset;
    [SerializeField] GameObject templateWindowPrefab;
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
        FetchDoor();
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
        g = Instantiate(templateWindowPrefab, transform);
        g.name = item.name;
        g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = item.imagePreview;
        //g.transform.GetChild(0).GetChild(1).GetComponent<LayoutElement>().preferredHeight = 800;

        g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;

        g.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            DoorPrefabToFit(item);
        
        });

        g.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            GetDoorPrefab(item);
        });
 
    }

    private void DoorPrefabToFit(ScriptableObjectsController item)
    {
        //item3DViewer.SetActive(true);
        
        CurrentPrefabController.ins.Door3DSelector.ItemPrefab = item.prefab;
        item3DViewer.SetActive(false);
        itemsGallery.SetActive(false);
    }

    private void FetchDoor()
    {
        doorAsset = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/door_bundle");

        if (doorAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        itemList.AddRange(doorAsset.LoadAllAssets<ScriptableObjectsController>());
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
            FindDoorPrefab(itemList.Find(i => i.name == arg0));
        }
    }

    public void GetDoorPrefab(ScriptableObjectsController item) {


        item3DViewer.SetActive(true);
        itemsGallery.SetActive(false);
        OnItemSelected?.Invoke(this, item);
    }

    private void DestroyAllPrefabs() {

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }

    public void FindDoorPrefab(ScriptableObjectsController item) {
        DestroyAllPrefabs();

        SetupInstance(item);
      
    }


}
