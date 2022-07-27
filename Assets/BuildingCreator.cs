using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] Building _building;

    void Start()
    {
        CreateNewBuilding();       
    }

    [ContextMenu("Create New Building")]
    void CreateNewBuilding()
    {
        if(_building == null)
        {
            Debug.Log("Creating new building");
            _building = new Building();
            GameManager.ins.Building = _building;
        }
        else
        {
            Debug.Log("Building already exist");
        }
        
    }
}
