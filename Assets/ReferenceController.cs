using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceController : MonoBehaviour
{
    public static ReferenceController ins { get; private set; }

    [SerializeField] GalleryOfItems _galleryOfItems;
    [SerializeField] Item3DViewer _item3DViewer;
    [SerializeField] EquipmentInsertionMode _equipmentInsertionMode;
    [SerializeField] ItemInspectionCamera _itemInspectionCamera;
    [SerializeField] GallerySearchField _gallerySearchField;
    [SerializeField] ModeController _modeController;
    [SerializeField] StoreySwitcherDropdown _storeySwitcherDropdown;
    [SerializeField] WhiteboardBackgroundInfo _whiteboardBackgroundInfo;
    public GalleryOfItems GalleryOfItems => _galleryOfItems;
    public Item3DViewer Item3DViewer => _item3DViewer;
    public EquipmentInsertionMode EquipmentInsertionMode => _equipmentInsertionMode;
    public ItemInspectionCamera ItemInspectionCamera => _itemInspectionCamera;
    public GallerySearchField GallerySearchField => _gallerySearchField;
    public ModeController ModeController => _modeController;
    public StoreySwitcherDropdown StoreySwitcherDropdown => _storeySwitcherDropdown;
    public WhiteboardBackgroundInfo WhiteboardBackgroundInfo => _whiteboardBackgroundInfo;

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
}
