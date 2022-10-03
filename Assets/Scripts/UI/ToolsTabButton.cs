using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ToolsTabButton : MonoBehaviour, IPointerClickHandler
{
    public ToolsTabGroup tabGroup;
    public int index;
    public Image background;


    public void OnPointerClick(PointerEventData eventData)
    {

        tabGroup.onTabSelected(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

}
