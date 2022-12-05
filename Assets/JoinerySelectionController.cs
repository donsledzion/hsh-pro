using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinerySelectionController : MonoBehaviour
{    
    [SerializeField] List<SelectedItem> _selectedJoinery;
    [SerializeField] GameObject _selectedItemPrefab;
    protected Transform _selection;
    [SerializeField] Material _selectedMaterial;
    [SerializeField] LayerMask _layerMask;

    private bool _objectIsOn;

    RaycastHit _hit;
    private void Update()
    {
        _selection = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask))
        {
            _selection = _hit.transform;
        }

        if (_selection != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectItem(_selection.GetComponent<Joinery>());
            }
        }
    }

    public void SelectItem(Joinery item)
    {
        foreach (SelectedItem i in _selectedJoinery)
        {
            if (i.Phantom == item.gameObject)
            {
                UnselectItem(i);
                return;
            }
        }
        GameObject duplicate = Instantiate(item.gameObject, item.transform.parent);
        RedrawMaterials(duplicate.transform, _selectedMaterial);

        GameObject selectedGameObject = Instantiate(_selectedItemPrefab, item.transform);
        SelectedItem selectedItem = selectedGameObject.GetComponent<SelectedItem>();
        selectedItem.Phantom = duplicate;
        selectedItem.SelectedGameObject = item.gameObject;
        item.gameObject.SetActive(false);
        _selectedJoinery.Add(selectedItem);
    }

    public void UnselectItem(SelectedItem item)
    {
        item.SelectedGameObject.SetActive(true);
        Destroy(item.Phantom);
        _selectedJoinery.Remove(item);
    }

    [ContextMenu("UnselectAll")]
    public void UnselectAll()
    {
        foreach (SelectedItem item in _selectedJoinery)
        {
            item.SelectedGameObject.SetActive(true);
            Destroy(item.Phantom);
        }
        _selectedJoinery.Clear();
    }


    [ContextMenu("Delete Selected")]
    public void DeleteSelected()
    {
        foreach (SelectedItem item in _selectedJoinery)
        {
            /*item.SelectedGameObject.SetActive(true);
            string guid = item.SelectedGameObject.GetComponent<Joinery>().GUID;
            Debug.Log("Trying to remove equipment item with guid: " + guid);
            if (GameManager.ins.Building.RemoveEquipmentItem(guid))
            {
                Debug.Log("Equipment item with guid: " + guid + " removed from model successfuly");
            }
            else
            {
                Debug.LogWarning("Could not remove equipment with guid: " + guid);
            }
            Destroy(item.Phantom);
            Destroy(item.SelectedGameObject);*/

            WallSectionAlt section3D = item.GetComponentInParent<WallSectionAlt>();
            Jamb jamb = section3D.Section as Jamb;
            if (jamb != null)
            {
                Debug.Log("Found Jamb");
                jamb.RemoveJoinery();
            }
            else
            {
                Debug.Log("Couldn't find Jamb!");
            }

            Destroy(item.Phantom);
            Destroy(item.SelectedGameObject);

        }
        _selectedJoinery.Clear();

    }

    void RedrawMaterials(Transform _trasform, Material material)
    {
        foreach (MeshRenderer renderer in _trasform.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = material;
        }
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }


    private void OnDisable()
    {
        UnselectAll();
    }
}
