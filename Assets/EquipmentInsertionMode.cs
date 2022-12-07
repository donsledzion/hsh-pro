using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif 


public class EquipmentInsertionMode : MonoBehaviour
{
    public static EquipmentInsertionMode ins { get; private set; }

    [SerializeField] string _assetName = "";
    [SerializeField] string _bundleName = "";

    [SerializeField] GameObject _equipmentPrefab;
    [SerializeField] GameObject _equipmentInstance;

    [SerializeField] Material _materialGood;
    [SerializeField] Material _materialBad;

    [SerializeField] LayerMask _layerMask;
    [SerializeField] LayerMask _collisionLayerMask;
    protected Transform _selection;
    bool _inserting = true;

    public string AssetName { get { return _assetName; } set { _assetName = value; } }
    public string BundleName { get { return _bundleName; } set { _bundleName = value; } }
    public GameObject EquipmentPrefab
    {
        get { return _equipmentPrefab; }
        set { _equipmentPrefab = value; }
    }

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

    RaycastHit _hit;
    private void OnEnable()
    {
        AssignPrefabLayerMask();
    }

    public void SwapPrefab(GameObject prefab)
    {
        _equipmentPrefab = prefab;
        Destroy(_equipmentInstance);
        _equipmentInstance = null;
        _inserting = true;
    }

    void Update()
    {
        if (!_inserting) return;

        _selection = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask))
        {
            _selection = _hit.transform;
        }

        if (_equipmentInstance != null)
        {
            if(_hit.point != null)
            _equipmentInstance.transform.position = 
                new Vector3(
                    _hit.point.x,
                    /*GameManager.ins.Building.CurrentStorey.Elevation*/ _hit.point.y,
                    _hit.point.z);
            if(_equipmentInstance.GetComponent<EquipmentItem>().IsColliding)
            {                
                RedrawMaterials(_equipmentInstance.transform, _materialBad);
            }
            else
            {
                RedrawMaterials(_equipmentInstance.transform, _materialGood);
            }

            if (Input.GetKeyDown(KeyCode.C))
                AbortInsertion();
            if (Input.GetKeyDown(KeyCode.R))
                RotateItem();
            if (Input.GetMouseButtonDown(0))
                InsertItem();
                  

        }
        

        if(_selection != null)
        {
            if(_equipmentInstance == null)
            {
                _equipmentInstance = Instantiate(_equipmentPrefab,_selection);
                SetCollidersTrigger(_equipmentInstance, true);
                WallEquipmentItem item = _equipmentInstance.GetComponent<WallEquipmentItem>();
                if (item != null)
                    item.enabled = true;
            }
        }
        else if(_equipmentInstance != null)
        {
            DisposeOfEquipmentInstance();
        }
    }

    void SetCollidersTrigger(GameObject gameObject, bool state)
    {
        foreach(Collider collider in gameObject.GetComponentsInChildren<Collider>())
            collider.isTrigger = state;
    }

    private void AbortInsertion()
    {
        _inserting = false;
        _selection = null;
        Destroy(_equipmentInstance);
        _equipmentInstance = null;
        gameObject.SetActive(false);
    }

    private void InsertItem()
    {

        GameObject insertedItem = null;
        if (_equipmentInstance.GetComponent<EquipmentItem>().GetType() == typeof(WallEquipmentItem))
        {
            WallEquipmentItem item = _equipmentInstance.GetComponent<WallEquipmentItem>();
            if (!item.IsColliding)
            {
                insertedItem = Instantiate(_equipmentPrefab, _selection);                
                insertedItem.GetComponent<WallEquipmentItem>().enabled = false;
                insertedItem.transform.position = item.MountTransform.position;
                insertedItem.transform.rotation = item.MountTransform.rotation;                
            }
            Destroy(item.gameObject);
        }
        else if(!_equipmentInstance.GetComponent<EquipmentItem>().IsColliding)
        {
            insertedItem = Instantiate(_equipmentPrefab, _selection);
            insertedItem.transform.position = _equipmentInstance.transform.position;
            insertedItem.transform.rotation = _equipmentInstance.transform.rotation;
        }
        if (insertedItem == null) return;
        StoreItemIntoModel(insertedItem.transform);
        DisposeOfEquipmentInstance();
        gameObject.SetActive(false);
    }

    void StoreItemIntoModel(Transform itemTransform)
    {
        Equipment equipment = GameManager.ins.Building.CurrentStorey.
            AddNewEquipment(
            Guid.NewGuid().ToString("N"),
            AssetName,
            BundleName,
            itemTransform.position,
            itemTransform.eulerAngles
            );
        EquipmentItem equipmentItem = itemTransform.GetComponent<EquipmentItem>();

        equipmentItem.GUID = equipment.GUID;
        equipmentItem.Equipment = equipment;
    }

    private void RotateItem()
    {
        _equipmentInstance.transform.Rotate(Vector3.up, 90);
    }

    void DisposeOfEquipmentInstance()
    {
        _selection = null;
        Destroy(_equipmentInstance);
        _equipmentInstance = null;
    }

    [ContextMenu("Assign Prefab Layer Mask")]
    void AssignPrefabLayerMask()
    {
        if (_equipmentPrefab != null)
        {
            _layerMask = _equipmentPrefab.GetComponent<EquipmentItem>().TargetLayer;
            _collisionLayerMask = _equipmentPrefab.GetComponent<EquipmentItem>().CollisionLayer;
        }
    }

    void RedrawMaterials(Transform _trasform, Material material)
    {
        foreach(Renderer renderer in _trasform.GetComponentsInChildren<Renderer>())
        {
            renderer.material = material;
        }

    }
}
