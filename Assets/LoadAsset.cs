using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAsset : MonoBehaviour
{
    public string assetName;
    [SerializeField] Material _loadedMaterial;
    [ContextMenu("Load texture")]
    void LoadTexture()
    {
        AssetBundle bundle = AssetBundleLoader.ins.WallSurfacesBundle.LoadBundle();

        if (bundle != null)
        {
            Debug.Log("Bundle loaded. Loading asset: " + assetName);
            ScriptableObjectsController item =  bundle.LoadAsset(assetName) as ScriptableObjectsController;
            _loadedMaterial = item.material;
        }
        else
        {
            Debug.LogError("Couldn't load asset! Bundle is null!");
        }
    }
}
