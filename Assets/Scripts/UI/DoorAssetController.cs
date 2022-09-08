using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorAssetController : MonoBehaviour
{

    public System.EventHandler<DoorScriptableObjects> OnItemSelected;

    private DoorScriptableObjects [] itemList;
    private AssetBundle doorAsset;
    GameObject templateWindow;
    GameObject itemPrefab;
    public Sprite sprite;

    private void Awake()
    {
        doorAsset = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/doorassetso");

        if (doorAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        itemList = doorAsset.LoadAllAssets<DoorScriptableObjects>();

        templateWindow = transform.GetChild(0).gameObject;

        GameObject g;
        foreach (DoorScriptableObjects item in itemList)
        {
            g = Instantiate(templateWindow, transform);
            g.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = item.imagePreview as Sprite;
            g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
            g.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                getDoorPrefab(item);
            });


        }

        Destroy(templateWindow);
    }

    public void getDoorPrefab(DoorScriptableObjects item) {

        OnItemSelected?.Invoke(this, item);
    }


}
