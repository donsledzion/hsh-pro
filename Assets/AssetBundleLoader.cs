using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] string assetDirectory;
    private AssetBundle doorAsset;
    void Start()
    {
        //doorAsset = AssetBundle.LoadFromFile(assetDirectory);

        if (doorAsset) Debug.Log("Loaded successfuly");
        else Debug.Log("Failed to load");

        AssetBundle bundle = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/door_bundle");
        if(bundle != null)
        {
            ScriptableObjectsController item = bundle.LoadAsset("JJ_Door_1") as ScriptableObjectsController;

            GameObject instance = Instantiate(item.prefab,gameObject.transform);
            

            instance.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(-100f,50f));

        }


    }

    

    void Update()
    {
        
    }
}
