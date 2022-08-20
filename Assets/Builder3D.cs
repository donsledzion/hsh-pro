using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class Builder3D : MonoBehaviour
{
    [SerializeField] GameObject wallSectionPrefab;

    
    void GenerateStorey(Storey storey)
    {
        foreach(Wall wall in storey.Walls)
        {
            foreach(WallSection section in wall.WallSections)
            {
                GameObject sectionObject = Instantiate(wallSectionPrefab, gameObject.transform);
                WallSectionMeshGenerator meshGenerator = sectionObject.GetComponent<WallSectionMeshGenerator>();
                meshGenerator.SetStoreyParams(storey);
                meshGenerator.GenerateSectionMesh(section);
            }
        }
    }

    [ContextMenu("Generate Current Storey")]
    public void GenerateCurrentStorey()
    {
        GenerateStorey(GameManager.ins.Building.CurrentStorey);
    }

    public void GenerateBuilding()
    {
        Building building = GameManager.ins.Building;
        foreach (Storey storey in building.Storeys)
            GenerateStorey(storey);
    }
}
