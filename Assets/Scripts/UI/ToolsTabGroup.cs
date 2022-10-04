using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsTabGroup : MonoBehaviour
{
    public List<ToolsTabButton> tabButtons;
    public Color tabIdle;
    public Color tabhover;
    public Color tabActive;
    public ToolsTabButton selectedTab;
    public List<GameObject> objectsToSwap;
    private Object[] textures;
    //private AddressablesController addressablesController;

    public void Subscribe(ToolsTabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<ToolsTabButton>();
        }

        tabButtons.Add(button);
    }
    public void onTabEnter(ToolsTabButton button)
    {
        if (selectedTab == null || button != selectedTab)
        {
            ResetTabs();

        }
    }

    public void onTabExit(ToolsTabButton button)
    {
        ResetTabs();
    }

    public void onTabSelected(ToolsTabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
        int index = button.index;
        for (int i = 0; i < objectsToSwap.Count; i++)
        {

            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (ToolsTabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }

    public void ResetObjectsToSwap()
    {

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
           objectsToSwap[i].SetActive(false);
        }
    }


}
