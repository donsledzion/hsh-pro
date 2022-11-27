using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyer : MonoBehaviour
{
    [SerializeField] Builder3D builder3D;

    [ContextMenu("DestroyBuilding")]
    public void DestroyBuilding()
    {
        builder3D.EraseStoreyDrawings();
        Drawing2DController.ins.EraseStoreys();
        GameManager.ins.Building = null;
        BuildingCreator.Instance.RemoveBuilding();
        //BuildingCreator.Instance.CreateNewBuilding();
    }
}
