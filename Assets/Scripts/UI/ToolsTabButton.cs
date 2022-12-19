using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ToolsTabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public ToolsTabGroup tabGroup;
    public int index;
    public Image background;
    [SerializeField]
    private bool _isOn = false;
    public bool isOn
    {
        get { return _isOn; }

        set { _isOn = value; }
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        tabGroup.onTabSelected(this);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.onTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.onTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

}
