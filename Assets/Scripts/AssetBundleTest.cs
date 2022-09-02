using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class AssetBundleTest : MonoBehaviour
{
    AssetBundle assetBundle;
    public GameObject windowSprite;
    GameObject templateWindow;

    public void getAssetTextures() {
        assetBundle = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/walltexturesbundle");
        Sprite[] wallTextures = assetBundle.LoadAllAssets<Sprite>();
        var wallTextureWindow = assetBundle.LoadAsset("Wall Texture Template");
        var j = 0;
        if (wallTextures!=null)
        {
            Debug.Log("Loaded successfuly");
        }
        else Debug.Log("Failed");


        templateWindow = transform.GetChild(0).gameObject;
        
        GameObject g;
        foreach (Sprite i in wallTextures) {

            
            g = Instantiate(templateWindow, transform);
            g.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = i;
          // windowSprite.GetComponent<Image>().sprite = i;
            j++; 
            
            Debug.Log(i+" "+j); 
        }

        //Destroy(templateWindow);
        
    }

    private void getTexturesFromBundle() { 

    }

}
