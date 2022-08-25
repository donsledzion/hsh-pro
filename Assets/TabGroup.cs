using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButtonGallery> tabButtons;
    public Sprite tabIdle;
    public Sprite tabhover;
    public Sprite tabActive;

    public void Subscribe(TabButtonGallery button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButtonGallery>();
            tabButtons.Add(button);
        }
    }
    public void onTabEnter(TabButtonGallery button)
    {
        ResetTabs();
        button.background.sprite = tabhover;
    }
    
    public void onTabExit(TabButtonGallery button)
    {
        ResetTabs();
    }
    
    public void onTabSelected(TabButtonGallery button)
    {
        ResetTabs();
        button.background.sprite = tabActive;

    }

    public void ResetTabs()
    {
        foreach (TabButtonGallery button in tabButtons)
        {
            button.background.sprite = tabIdle;
        }
    }
}
