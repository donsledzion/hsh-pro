using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSectionJamb : WallSectionAlt
{

    [SerializeField] protected PhantomScaler _phantomScaler;
    protected GameObject FitPrefabIntoJamb(string itemName, AssetBundle bundle)
    {
        if (bundle != null)
        {
            ScriptableObjectsController item = bundle.LoadAsset(itemName) as ScriptableObjectsController;

            GameObject instance = Instantiate(item.prefab, gameObject.transform);


            Vector3 prefabSize = instance.GetComponent<BoxCollider>().size;
            Vector3 scaleFactor = _phantomScaler.transform.localScale;
            Vector3 jambSize = _phantomScaler.RendererTransform.GetComponent<BoxCollider>().size;
            instance.transform.localScale = new Vector3(
                scaleFactor.x * jambSize.x / prefabSize.x,
                scaleFactor.y * jambSize.y / prefabSize.y,
                10 * jambSize.z / prefabSize.z);
            instance.transform.SetParent(_phantomScaler.RendererTransform);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;

            _phantomScaler.RendererTransform.GetComponent<MeshRenderer>().enabled = false;
            _phantomScaler.RendererTransform.GetComponent<BoxCollider>().enabled = false;
            instance.GetComponent<BoxCollider>().enabled = false;
        }
        return null;
    }

    public void InsertJoinery()
    {
        if (Section == null)
        {
            Debug.LogError("No section assigned to WallSectionDoorjamb! Aborting");
            return;
        }
        Jamb jamb = Section as Jamb;
        string itemName = jamb.JoineryName;
        if (itemName != "" && itemName != null)
        {
            AssetBundle bundle;
            if (this.GetType() == typeof(WallSectionDoorjamb))
                bundle = AssetBundleLoader.ins.DoorBundle.LoadBundle();
            else if (this.GetType() == typeof(WallSectionWindowjamb))
                bundle = AssetBundleLoader.ins.WindowsBundle.LoadBundle();
            else
                return;
            FitPrefabIntoJamb(itemName, bundle);
        }
    }
}
