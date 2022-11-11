using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextureAssetController : MonoBehaviour
{
    public System.EventHandler<ScriptableObjectsController> OnItemSelected;
    public System.EventHandler<ScriptableObjectsController> OnItemChoosen;

    private List<ScriptableObjectsController> itemList = new List<ScriptableObjectsController>();
    private AssetBundle textureAsset;
    [SerializeField] GameObject templateWindowPrefab;
    [SerializeField] GameObject inspectionCamera;
    [SerializeField] GameObject item3DViewer;
    [SerializeField] GameObject itemsGallery;
    [SerializeField] GameObject surfaceSelector;



    GameObject itemPrefab;
    public TMP_InputField inputField;

    private void Awake()
    {
        Init();
    }

/*    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Its over UI elements");
        }
        else
        {
            Debug.Log("Its NOT over UI elements");
        }
    }*/

    private void Init()
    {
        FetchTextures();
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
        g.transform.GetChild(0).GetChild(1).GetComponent<LayoutElement>().preferredHeight = 300;
        g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;

        g.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            TextureToApply(item);


        });

        g.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {;
            inspectionCamera.SetActive(true);
            GetTexture(item);
        });
    }

    private void TextureToApply(ScriptableObjectsController item)
    {

        surfaceSelector.GetComponent<FittingModeSwitcher>().WallsPaintingMode();

        item3DViewer.SetActive(false);
        itemsGallery.SetActive(false);
        CurrentMaterialController.ins.SurfaceSelector.SelectionMaterial = item.material;
    }

    private void FetchTextures()
    {
        textureAsset = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/texture_bundle");

        if (textureAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        itemList.AddRange(textureAsset.LoadAllAssets<ScriptableObjectsController>());
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

    public void GetTexture(ScriptableObjectsController item)
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
