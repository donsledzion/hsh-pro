using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    public static AssetBundleLoader ins { get; private set; }

    public BundleLoadStatus DoorBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/door_bundle");
    public BundleLoadStatus WindowsBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/windowassets");



    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }


}

[Serializable]
public class BundleLoadStatus
{
    public string BundlePath;
    public bool IsLoaded;
    public AssetBundle Bundle;

    public BundleLoadStatus()
    {
        BundlePath = "";
        IsLoaded = false;
        Bundle = null;
    }

    public BundleLoadStatus(string bundlePath)
    {
        BundlePath = bundlePath;
        IsLoaded = false;
        Bundle = null;
    }

    public AssetBundle LoadBundle()
    {
        if(IsLoaded)
        {
            Debug.LogWarning("Bundle is already loaded");
            return Bundle;
        }
        if(BundlePath == "")
        {
            Debug.LogError("Could not load bundle: empty path");
            return null;
        }
        Bundle = AssetBundle.LoadFromFile(BundlePath);

        if(Bundle == null)
        {
            Debug.LogError("Bundle load returned null!");
            return null;
        }
        IsLoaded = true;
        return Bundle;

    }
}
