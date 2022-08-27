using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class Builder3D : MonoBehaviour
{
    [SerializeField] GameObject wallSectionPrefab;
    [SerializeField] GameObject ceilingSectionPrefab;
    
    void GenerateStorey(Storey storey)
    {
        /*
         * TODO:
         * create wall and ceiling sections in separate transforms depending on the storey they belong to
         */
        foreach (Wall wall in storey.Walls)
        {
            foreach(WallSection section in wall.WallSections)
            {
                GameObject sectionObject = Instantiate(wallSectionPrefab, gameObject.transform);
                WallSectionAlt sectionAlt = sectionObject.GetComponent<WallSectionAlt>();
                sectionAlt.SetParameters(storey, wall, section);
                sectionAlt.Spatialize(section);                
            }
        }
        foreach(Ceiling ceiling in storey.Ceilings)
        {
            GameObject ceilingObject = Instantiate(ceilingSectionPrefab);
            ceilingObject.transform.SetParent(gameObject.transform);
            CeilingSection ceilingSection = ceilingObject.GetComponent<CeilingSection>();
            foreach(CeilingPlane plane in ceilingSection.CeilingPlanes)
            {
                plane.SetParameters(ceiling);
                plane.Spatialize();
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
        EraseStoreyDrawings();
        Building building = GameManager.ins.Building;
        foreach (Storey storey in building.Storeys)
            GenerateStorey(storey);
    }

    void EraseStoreyDrawings()
    {
        foreach(Transform _transform in gameObject.GetComponentsInChildren<Transform>())
        {
            if(_transform != transform)
            {
                Destroy(_transform.gameObject);
            }
        }
    }
}
