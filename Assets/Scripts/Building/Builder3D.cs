using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class Builder3D : MonoBehaviour
{
    [SerializeField] GameObject _sectionStraightPrefab;
    [SerializeField] GameObject _sectionDoorjambPrefab;
    [SerializeField] GameObject _sectionWindowjambPrefab;
    [SerializeField] GameObject _ceilingSectionPrefab;
    [SerializeField] GameObject _floorSectionPrefab;
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _enviornmentTransform;
    [SerializeField] GameObject cornerFinishingPointPrefab;
    StoreyPointsCollector _storeyPointsCollector;

    private void Start()
    {
        _storeyPointsCollector = GetComponent<StoreyPointsCollector>();
    }

    void GenerateStorey(Storey storey)
    {
        /*
         * TODO:
         * create wall and ceiling sections in separate transforms depending on the storey they belong to
         */
        //Generating walls (wall may contain several wall sections
        foreach (Wall wall in storey.Walls)
        {
            //Generating wall sections depending on wall section type
            foreach (WallSection section in wall.WallSections)
            {
                if (section is SectionStraight)
                {
                    GameObject sectionObject = Instantiate(_sectionStraightPrefab, gameObject.transform);
                    WallSectionAlt sectionAlt = sectionObject.GetComponent<WallSectionAlt>();
                    sectionAlt.SetParameters(storey, wall, section);
                    sectionAlt.Spatialize(section);
                }
                else if(section is Doorjamb)
                {
                    GameObject sectionObject = Instantiate(_sectionDoorjambPrefab, gameObject.transform);
                    WallSectionDoorjamb sectionJamb = sectionObject.GetComponent<WallSectionDoorjamb>();
                    sectionJamb.SetParameters(storey, wall, (Doorjamb)section);
                    sectionJamb.Spatialize((Doorjamb)section);
                    sectionJamb.InsertJoinery();
                }
                else if(section is Windowjamb)
                {
                    GameObject sectionObject = Instantiate(_sectionWindowjambPrefab, gameObject.transform);
                    WallSectionWindowjamb sectionJamb = sectionObject.GetComponent<WallSectionWindowjamb>();
                    sectionJamb.SetParameters(storey, wall, (Windowjamb)section);
                    sectionJamb.Spatialize((Windowjamb)section);
                    sectionJamb.InsertJoinery();
                }
            }
        }
        //Generating inter-storey ceilings
        foreach(Ceiling ceiling in storey.Ceilings)
        {
            GameObject ceilingObject = Instantiate(_ceilingSectionPrefab);
            ceilingObject.transform.SetParent(gameObject.transform);
            CeilingSection ceilingSection = ceilingObject.GetComponent<CeilingSection>();
            foreach (CeilingPlane plane in ceilingSection.CeilingPlanes)
            {
                plane.SetParameters(ceiling);
                plane.Spatialize();
            }
            foreach (CeilingBand band in ceilingSection.CeilingBands)
            {
                band.SetParameters(ceiling);
                band.Spatialize();
            }            
        }
        // Inserting floor planes
        foreach(FloorSection2D floor in storey.Floors)
        {
            GameObject floorObject = Instantiate(_floorSectionPrefab);
            floorObject.transform.SetParent(gameObject.transform);
            FloorSection floorSection = floorObject.GetComponent<FloorSection>();
            foreach (FloorPlane plane in floorSection.FloorPlanes)
            {
                plane.SetParameters(floor);
                plane.Spatialize();
                plane.gameObject.AddComponent<MeshCollider>();
            }
        }
        // Inserting "corners" (rounded so far) on wall connections
        foreach(StoreyPointsCollector.ConnectorPoint connectorPoint in _storeyPointsCollector.ListConnectorPoints(storey))
        {
            GameObject corner = Instantiate(cornerFinishingPointPrefab);
            corner.transform.SetParent(gameObject.transform);
            float size = connectorPoint.ThickestWallThickness();
            corner.transform.localScale = new Vector3(size, storey.Height, size);
            corner.transform.localPosition = new Vector3(connectorPoint.Point.x,storey.Elevation,connectorPoint.Point.y) - transform.localPosition;            
        }
        //Inserting flooring under first floor (independand to floor secions)
        // So far only solution is to generate rectangular flooring covering whole building
        Vector2[] flooringPoints = PolygonHelper.RangeToRect(
            PolygonHelper.PlaneRange(
                _storeyPointsCollector.WallPointsListToVectorArray(
                    _storeyPointsCollector.CollectAllPoints(
                        GameManager.ins.Building.Storeys[0]))),50f);
        GameObject flooringObject = Instantiate(_ceilingSectionPrefab);
        flooringObject.transform.SetParent(gameObject.transform);
        CeilingSection flooringSection = flooringObject.GetComponent<CeilingSection>();
        Ceiling flooring = new Ceiling(10f,0f,flooringPoints);
        CeilingPlane flooringPlane = flooringSection.CeilingPlanes[0];
        flooringPlane.SetParameters(flooring);
        flooringPlane.Spatialize();
        


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
        foreach (Transform _transform in gameObject.GetComponentsInChildren<Transform>())
        {
            if(_transform != transform && _transform.parent == transform && _transform != _playerTransform && _transform != _enviornmentTransform)
            {
                Destroy(_transform.gameObject);
            }
        }
    }
}
