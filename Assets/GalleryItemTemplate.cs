using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryItemTemplate : MonoBehaviour
{
    [SerializeField] Image _thumbnail;
    [SerializeField] LayoutElement _layoutElement;
    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] Button _selectButton;
    [SerializeField] Button _previewButton;

    public Image Thumbnail { get { return _thumbnail; } }
    public LayoutElement LayoutElement { get { return _layoutElement; } }
    public TextMeshProUGUI ItemNameTMPro { get { return _itemName; } }
    public Button SelectButton { get { return _selectButton; } }
    public Button PreviewButton { get { return _previewButton; } }
}
