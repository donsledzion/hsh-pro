using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI.Extensions.Tweens;

public class AssetBundleLoader : MonoBehaviour
{
    public static AssetBundleLoader ins { get; private set; }

    [SerializeField] bool loadOnStart = false;

    public BundleLoadStatus BathroomBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/bathroom_bundle");
    public BundleLoadStatus BedroomBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/bedroom_bundle");
    public BundleLoadStatus DoorBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/door_bundle");
    public BundleLoadStatus FloorSurfacesBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/floor_bundle");
    public BundleLoadStatus FurnitureArmchairsBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_armchairs");
    public BundleLoadStatus FurnitureBedsBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_beds");
    public BundleLoadStatus FurnitureCoachiesBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_coachies");
    public BundleLoadStatus FurnitureCornersBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_corners");
    public BundleLoadStatus FurnitureDiningRoomBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_dining_room");
    public BundleLoadStatus FurnitureKitchenBaseCabinetsRoomBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_kitchen_base_cabinets");
    public BundleLoadStatus FurnitureLivingRoomBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_living_room");
    public BundleLoadStatus FurnitureSofasBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/furniture_bundle_sofas");
    public BundleLoadStatus WindowsBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/windowassets");
    public BundleLoadStatus WallSurfacesBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/texture_bundle");
    public BundleLoadStatus CeilingLightsBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/ceiling_lights_bundle");
    public BundleLoadStatus FloorLightsBundle = new BundleLoadStatus("AssetBundles/StandaloneWindows/floor_lights_bundle");




    public List<BundleLoadStatus> bundlesPack = new List<BundleLoadStatus>();

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


    private void Start()
    {
        if(loadOnStart)
        {
            bundlesPack.Clear();
            bundlesPack.Add(BathroomBundle);
            bundlesPack.Add(BedroomBundle);
            bundlesPack.Add(DoorBundle);
            bundlesPack.Add(FloorSurfacesBundle);
            bundlesPack.Add(FurnitureArmchairsBundle);
            bundlesPack.Add(FurnitureBedsBundle);
            bundlesPack.Add(FurnitureCoachiesBundle);
            bundlesPack.Add(FurnitureCornersBundle);
            bundlesPack.Add(FurnitureDiningRoomBundle);
            bundlesPack.Add(FurnitureKitchenBaseCabinetsRoomBundle);
            bundlesPack.Add(FurnitureLivingRoomBundle);
            bundlesPack.Add(FurnitureSofasBundle);
            bundlesPack.Add(WindowsBundle);
            bundlesPack.Add(WallSurfacesBundle);
            bundlesPack.Add(CeilingLightsBundle);
            bundlesPack.Add(FloorLightsBundle);


            GetComponent<BundleLoadingPrompt>().ShowLoadingPrompt();
            foreach (BundleLoadStatus bundle in bundlesPack)
                StartCoroutine(LoadBundleAsync(bundle));
        }
    }

    public bool AreAllBundlesLoaded()
    {
        foreach(BundleLoadStatus bundle in bundlesPack)
            if (!bundle.IsLoaded) return false;
        return true;
    }

    IEnumerator LoadBundleAsync(BundleLoadStatus bundleStatus)
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, bundleStatus.BundlePath);
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(path);
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }        
        bundleStatus.Bundle = myLoadedAssetBundle;
        bundleStatus.IsLoaded = true;
    }

    public BundleLoadStatus GetBundleLoadStatusByName(string name)
    {
        foreach (BundleLoadStatus bundle in bundlesPack)
        {
            if (bundle.BundlePath.Contains(name))
                return bundle;
        }
        return null;
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
