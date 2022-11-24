using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreySwitcher : MonoBehaviour
{

    int currentStoreyIndex => StoreySwitcherDropdown.CurrentStoreyIndex();

    [SerializeField] TextMeshProUGUI _currentStoreyTMP;
    /// <summary>
    /// Method to switch current storey of Building into given by parameter.
    /// </summary>
    /// <param name="storeyIndex"></param>
    /// 
    void SwitchToStorey(int storeyIndex) 
    {
        
        storeyIndex=Mathf.Clamp(storeyIndex, 0, GameManager.ins.Building.Storeys.Count - 1);
        Debug.Log("Prze��cz na pi�tro : " + storeyIndex);
        GameManager.ins.Building.SetCurrentStorey(GameManager.ins.Building.Storeys[storeyIndex]);
        Drawing2DController.ins.SwitchToStorey(GameManager.ins.Building.CurrentStorey);
        
  
    }

    private void Update()
    {
        _currentStoreyTMP.text = currentStoreyIndex.ToString();
    }

    public void SwitchUp() 
    {
        Debug.Log("Prze��cz w g�r�");
        SwitchToStorey(currentStoreyIndex+1);
    }

    public void SwitchDown() 
    {
        Debug.Log("Prze��cz w d�");
        SwitchToStorey(currentStoreyIndex-1);
    }

}
