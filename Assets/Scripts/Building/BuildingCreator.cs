using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BuildingCreator : MonoBehaviour
{
    public static BuildingCreator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] Building _building;

    

    void Start()
    {
        CreateNewBuilding();       
    }

    public void RemoveBuilding()
    {
        _building = null;
    }

    [ContextMenu("Create New Building")]
    public void CreateNewBuilding()
    {
        if(_building == null)
        {
            Debug.Log("Creating new building");
            _building = new Building();
            _building.Storeys.Add(new Storey());
            _building.SetCurrentStorey(_building.Storeys[0]);
            GameManager.ins.Building = _building;
            Drawing2DController.ins.EraseStoreys();
            Drawing2DController.ins.InitializeFirstStorey(GameManager.ins.Building.CurrentStorey);
            Debug.Log(GameManager.ins.Building.ToString());
        }
        else
        {
            Debug.Log("Building already exist");
        }
        
    }
}
