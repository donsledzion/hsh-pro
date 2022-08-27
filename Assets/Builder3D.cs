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
        Debug.Log("Generating walls - " + storey.Walls.Count);
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
        Debug.Log("Generating ceilings - " + storey.Ceilings.Count);
        foreach(Ceiling ceiling in storey.Ceilings)
        {
            Debug.Log("Ceiling is generated - " + ceiling.ToString()); ;
            GameObject ceilingObject = Instantiate(ceilingSectionPrefab);
            ceilingObject.transform.SetParent(gameObject.transform);
            CeilingSection ceilingSection = ceilingObject.GetComponent<CeilingSection>();
            ceilingSection.SetParameters(ceiling);
            ceilingSection.Spatialize();
            Debug.Log("Finished generating ceiling");
        }
    }

    [ContextMenu("Generate Current Storey")]
    public void GenerateCurrentStorey()
    {
        GenerateStorey(GameManager.ins.Building.CurrentStorey);
    }

    public void GenerateBuilding()
    {
        Debug.Log("Generating building...");
        EraseStoreyDrawings();
        Debug.Log("Erased drawings");
        Building building = GameManager.ins.Building;
        Debug.Log("Assigned building, now starting the loop through storeys");
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
