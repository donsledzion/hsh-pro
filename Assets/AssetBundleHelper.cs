using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetBundleHelper
{
    public static string ExtractName(ScriptableObjectsController item)
    {
        string toRemove = " (" + item.GetType().ToString()+")";
        Debug.Log("Substring to remove:" + toRemove);
        return item.ToString().Replace(toRemove,"");
    }
}
