using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButtonGallery> tabButtons;
    public Color tabIdle;
    public Color tabhover;
    public Color tabActive;
    public TabButtonGallery selectedTab;
    public List<GameObject> objectsToSwap;
    private Object[] textures;

    public void Subscribe(TabButtonGallery button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButtonGallery>();
        }
        
        tabButtons.Add(button);
    }
    public void onTabEnter(TabButtonGallery button)
    {
        if (selectedTab == null || button != selectedTab)
        {
            ResetTabs();
            button.background.color = tabhover;
        }
    }
    
    public void onTabExit(TabButtonGallery button)
    {
        ResetTabs();
    }
    
    public void onTabSelected(TabButtonGallery button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i =0; i<objectsToSwap.Count; i++)
        {

            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
                textures = Resources.LoadAll("Wall textures", typeof(Texture2D));
                Debug.Log("jestem tu");
                foreach (var t in textures)
                {
                    Debug.Log(t.name + "jestem tu2");
                }
            }
            else {
                objectsToSwap[i].SetActive(false);
            }

        }

    }

    public void ResetTabs()
    {
        foreach (TabButtonGallery button in tabButtons)
        {
            if(selectedTab!=null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }
}
