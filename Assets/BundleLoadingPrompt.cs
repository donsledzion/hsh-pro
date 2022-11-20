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
        _startTime = Time.time;
        _loadingPrompt.gameObject.SetActive(true);
        yield return new WaitUntil(() => AssetBundleLoader.ins.AreAllBundlesLoaded());
        _loadingPrompt.gameObject.SetActive(false);
        Debug.Log("Loaded bundles in " + (Time.time - _startTime).ToString());
    }

}
