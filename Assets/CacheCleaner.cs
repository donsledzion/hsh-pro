using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheCleaner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ClearCacheCor());
    }

    IEnumerator ClearCacheCor()
    {
        if(!Caching.ClearCache())
            Debug.Log("Cache is being used");
        yield return new WaitUntil(() => Caching.ClearCache());
        Debug.Log("Successfully cleaned the cache");
        AssetBundle.UnloadAllAssetBundles(true);
    }
}
