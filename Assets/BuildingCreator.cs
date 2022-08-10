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
            Debug.Log("Drawing2DController.ins: " + Drawing2DController.ins.ToString());
            Drawing2DController.ins.InitializeFirstStorey(GameManager.ins.Building.CurrentStorey);
            Debug.Log(GameManager.ins.Building.ToString());
        }
        else
        {
            Debug.Log("Building already exist");
        }
        
    }
}
