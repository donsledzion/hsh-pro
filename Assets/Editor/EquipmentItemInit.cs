using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR.InteractionSystem;

public class EquipmentItemInit : Editor
{
    [SerializeField] static string[] equipmenItemTargetLayer = { "FloorSurface" };
    [SerializeField] static string[] equipmentItemCollisionLayer = { "SlotDoor","SlotWindow", "WallSurface", "Equipment" };
    [SerializeField] static string[] wallEquipmenItemTargetLayer = { "FloorSurface" };
    [SerializeField] static string[] wallEquipmentItemCollisionLayer = { "SlotDoor","SlotWindow", "Equipment" };
    [SerializeField] static string[] wallLayer = { "WallSurface" };

    [MenuItem("GameObject/CreateStaticEquipmentItem")]
    public static GameObject CreateStaticEquipmentItem()
    {
        GameObject go;

        Object prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);
        if (prefabRoot != null)
            go = (GameObject)prefabRoot;
        else
            go = Selection.activeGameObject;

        go.layer = LayerMask.NameToLayer("Equipment");

        LayerMask targetMask = LayerMask.GetMask(equipmenItemTargetLayer);
        LayerMask collisionMask = LayerMask.GetMask(equipmentItemCollisionLayer);

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
        collider.size = new Vector3(100, 100, 100);

        return go;
    }

    [MenuItem("GameObject/CreatePickableEquipmentItem")]
    public static GameObject CreatePickableEquipmentItem()
    {
        GameObject go = CreateStaticEquipmentItem();

        Throwable throwable = go.GetComponent<Throwable>();
        if(throwable == null)
            throwable = go.AddComponent<Throwable>();

        throwable.restoreOriginalParent = true;
        Interactable interactable = go.GetComponent<Interactable>();
        if(interactable == null)
            interactable = go.AddComponent<Interactable>();
        interactable.hideHandOnAttach = false;
        interactable.hideControllerOnAttach = true;
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        if (rigidbody == null)
            rigidbody = go.AddComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;

        return go;
    }

    [MenuItem("GameObject/CreateWallEquipmentItem")]
    public static GameObject CreatePickableFloorEquipmentItem()
    {
        GameObject go;

        Object prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);
        if (prefabRoot != null)
            go = (GameObject)prefabRoot;
        else
            go = Selection.activeGameObject;

        go.layer = LayerMask.NameToLayer("Equipment");

        LayerMask targetMask = LayerMask.GetMask(wallEquipmenItemTargetLayer);
        LayerMask collisionMask = LayerMask.GetMask(wallEquipmentItemCollisionLayer);
        LayerMask wallMask = LayerMask.GetMask(wallLayer);

        WallEquipmentItem equipmentItem = go.GetComponent<WallEquipmentItem>();
        if (equipmentItem == null)
            equipmentItem = go.AddComponent<WallEquipmentItem>();
        equipmentItem.SetCollisionLayerMask(collisionMask);
        equipmentItem.SetTargetLayerMask(targetMask);
        equipmentItem.SetWallLayerMask(wallMask);
        BoxCollider collider = go.GetComponent<BoxCollider>();
        if (collider == null)
            collider = go.AddComponent<BoxCollider>();
        collider.enabled = true;
        collider.isTrigger = true;
        collider.size = new Vector3(100, 100, 100);

        equipmentItem.enabled = false;

        return go;
    }

    

}
