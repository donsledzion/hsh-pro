using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class AssetBundleTest : MonoBehaviour
{
    AssetBundle assetBundle;
    GameObject templateWindow;
    Sprite[] sprites;
    TextAsset[] descriptions;
    GameObject[] prefabs;

    public void getAssetTextures() {

        if (!assetBundle)
        {

            assetBundle = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/walltexturesbundle");
            sprites = assetBundle.LoadAllAssets<Sprite>();
            descriptions = assetBundle.LoadAllAssets<TextAsset>();

            //Debug.Log(descriptions.Length);
            
            templateWindow = transform.GetChild(0).gameObject;

            GameObject g;
            for (int i=0; i<sprites.Length; i++)
            {
                g = Instantiate(templateWindow, transform);
                g.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = sprites[i];
                g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = descriptions[i].text;
            }

            Destroy(templateWindow);

        }
        
    }


    public void getDoorAsset()
    {

        if (!assetBundle)
        {

            assetBundle = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/door asset");
            prefabs = assetBundle.LoadAllAssets<GameObject>();
            descriptions = assetBundle.LoadAllAssets<TextAsset>();
            sprites = assetBundle.LoadAllAssets<Sprite>();

            templateWindow = transform.GetChild(0).gameObject;

            GameObject g;

            Debug.Log(sprites.Length);
            
            for (int i = 0; i < sprites.Length; i++)
            {

                g = Instantiate(templateWindow, transform);
                g.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = sprites[i];
                g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = descriptions[i].text;
            }
            
        }

            Destroy(templateWindow);

        }

    }



