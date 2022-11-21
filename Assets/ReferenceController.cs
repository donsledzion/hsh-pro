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
    public GalleryOfItems GalleryOfItems => _galleryOfItems;
    public Item3DViewer Item3DViewer => _item3DViewer;
    public EquipmentInsertionMode EquipmentInsertionMode => _equipmentInsertionMode;
    public ItemInspectionCamera ItemInspectionCamera => _itemInspectionCamera;
    public GallerySearchField GallerySearchField => _gallerySearchField;
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
