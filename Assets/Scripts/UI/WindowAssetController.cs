using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowAssetController : MonoBehaviour
{
    public System.EventHandler<ScriptableObjectsController> OnItemSelected;
    public System.EventHandler<ScriptableObjectsController> OnItemChoosen;


    private List<ScriptableObjectsController> itemList = new List<ScriptableObjectsController>();
    private AssetBundle windowAsset;
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
        FetchWindows();
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
        g.transform.GetChild(0).GetChild(1).GetComponent<LayoutElement>().preferredHeight = 400;
        g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;

        g.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            WindowPrefabToFit(item);

        });

        g.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            GetWindowPrefab(item);
        });
    }

    private void WindowPrefabToFit(ScriptableObjectsController item)
    {
        CurrentPrefabController.ins.Window3DSelector.ItemPrefab = item.prefab;
        item3DViewer.SetActive(false);
        itemsGallery.SetActive(false);
    }

    private void FetchWindows()
    {
        windowAsset = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/windowassets");

        if (windowAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        itemList.AddRange(windowAsset.LoadAllAssets<ScriptableObjectsController>());
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
            FindWindowPrefab(itemList.Find(i => i.name == arg0));
        }
    }

    public void GetWindowPrefab(ScriptableObjectsController item)
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

    public void FindWindowPrefab(ScriptableObjectsController item)
    {
        DestroyAllPrefabs();

        SetupInstance(item);

    }
}
