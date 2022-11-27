using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
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
    [SerializeField] StoreyPointsCollector _storeyPointsCollector;
    public GalleryOfItems GalleryOfItems => _galleryOfItems;
    public Item3DViewer Item3DViewer => _item3DViewer;
    public EquipmentInsertionMode EquipmentInsertionMode => _equipmentInsertionMode;
    public ItemInspectionCamera ItemInspectionCamera => _itemInspectionCamera;
    public GallerySearchField GallerySearchField => _gallerySearchField;
    public ModeController ModeController => _modeController;
    public StoreySwitcherDropdown StoreySwitcherDropdown => _storeySwitcherDropdown;
    public WhiteboardBackgroundInfo WhiteboardBackgroundInfo => _whiteboardBackgroundInfo;
    public StoreyPointsCollector StoreyPointsCollector => _storeyPointsCollector;

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
