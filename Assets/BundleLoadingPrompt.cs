using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleLoadingPrompt : MonoBehaviour
{
    [SerializeField] Transform _loadingPrompt;

    float _startTime;

    [ContextMenu("ShowLoadingPrompt")]
    public void ShowLoadingPrompt()
    {
        StartCoroutine(ShowLoadingPromptCor());
    }

    IEnumerator ShowLoadingPromptCor()
    {
        if (_loadingPrompt == null) ;
        _loadingPrompt = ((LoadingProgres)FindObjectsOfTypeAll(typeof(LoadingProgres))[0]).transform;
        _startTime = Time.time;
        _loadingPrompt.gameObject.SetActive(true);
        yield return new WaitUntil(() => AssetBundleLoader.ins.AreAllBundlesLoaded());
        if (_loadingPrompt == null) ;
        _loadingPrompt = ((LoadingProgres)FindObjectsOfTypeAll(typeof(LoadingProgres))[0]).transform;
        _loadingPrompt.gameObject.SetActive(false);
        Debug.Log("Loaded bundles in " + (Time.time - _startTime).ToString());
    }

}
