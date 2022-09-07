using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AddressablesController : MonoBehaviour
{
    [SerializeField]
    AssetReference assetReference;    
    
    [SerializeField]
    AssetLabelReference wallTexturesLabel;

    private List<IResourceLocation> _wallTextures;
    //AsyncOperationHandle<GameObject> loadHandle;

    void Start() {

        Addressables.LoadAssetsAsync<IResourceLocation>(wallTexturesLabel.labelString, null).Completed += SpriteLoaded;
    }


    public void getListOfItems() {


        

        //loadHandle.Completed += SpriteLoaded;

            /*Addressables.InstantiateAsync(assetReference, transform);

            Debug.Log("sssssssssssss");*/
        }


    private void SpriteLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj)
    {
        _wallTextures = new List<IResourceLocation>(obj.Result);

        //StartCoroutine(LoadAssetsToScene());
    }

}
