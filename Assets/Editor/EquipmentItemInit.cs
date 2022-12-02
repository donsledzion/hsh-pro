using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*public class EquipmentItemInit : Editor
{
    static string 

//    [MenuItem("GameObject/CreateEquipmentItem")]
//    public static void CreateEquipmentItem()
//    {
*//*        GameObject go;

        Object prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);
        if (prefabRoot != null)
            go = (GameObject)prefabRoot;
        else
            go = Selection.activeGameObject;

        LayerMask targetMask = new EquipmentItemInit().targetLayer;
        LayerMask collisionMask = new EquipmentItemInit().collisionLayer;

        EquipmentItem equipmentItem = go.GetComponent<EquipmentItem>();
        if(equipmentItem == null)
            equipmentItem = go.AddComponent<EquipmentItem>();
        equipmentItem.SetCollisionLayerMask(collisionMask);
        equipmentItem.SetTargetLayerMask(targetMask);
        BoxCollider collider = go.GetComponent<BoxCollider>();
        if (collider == null)
            collider = go.AddComponent<BoxCollider>();
        collider.enabled = true;
        collider.isTrigger = true;
    }

    
*//*
}*/
