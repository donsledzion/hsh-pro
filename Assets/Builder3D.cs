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
        Debug.Log("GENERATING STOREY!");
        foreach (Wall wall in storey.Walls)
        {
            Debug.Log("Spatializing walls...");
            foreach (WallSection section in wall.WallSections)
            {
                Debug.Log("Spatializing sections...");
                if (section is SectionStraight)
                {
                    Debug.Log("Spatializing STRAIGHT sections...");
                    GameObject sectionObject = Instantiate(_sectionStraightPrefab, gameObject.transform);
                    WallSectionAlt sectionAlt = sectionObject.GetComponent<WallSectionAlt>();
                    sectionAlt.SetParameters(storey, wall, section);
                    sectionAlt.Spatialize(section);
                }
                else if(section is Doorjamb)
                {
                    Debug.Log("Spatializing JAMB sections...");
                    GameObject sectionObject = Instantiate(_sectionDoorjambPrefab, gameObject.transform);
                    WallSectionDoorjamb sectionJamb = sectionObject.GetComponent<WallSectionDoorjamb>();
                    sectionJamb.SetParameters(storey, wall, (Doorjamb)section);
                    sectionJamb.Spatialize((Doorjamb)section);
                }
                else if(section is Windowjamb)
                {
                    Debug.Log("Spatializing JAMB sections...");
                    GameObject sectionObject = Instantiate(_sectionWindowjambPrefab, gameObject.transform);
                    WallSectionWindowjamb sectionJamb = sectionObject.GetComponent<WallSectionWindowjamb>();
                    sectionJamb.SetParameters(storey, wall, (Windowjamb)section);
                    sectionJamb.Spatialize((Windowjamb)section);
                }
            }
        }
        Debug.Log("Spatializing ceilings...");
        foreach(Ceiling ceiling in storey.Ceilings)
        {
            GameObject ceilingObject = Instantiate(_ceilingSectionPrefab);
            ceilingObject.transform.SetParent(gameObject.transform);
            CeilingSection ceilingSection = ceilingObject.GetComponent<CeilingSection>();
            Debug.Log("Spatializing planes...");
            foreach (CeilingPlane plane in ceilingSection.CeilingPlanes)
            {
                plane.SetParameters(ceiling);
                plane.Spatialize();
            }
            Debug.Log("spatializing bands...");
            foreach (CeilingBand band in ceilingSection.CeilingBands)
            {
                band.SetParameters(ceiling);
                band.Spatialize();
            }            
        }

        Debug.Log("Spatializing floors...( " + storey.Floors.Count + " )");
        foreach(FloorSection2D floor in storey.Floors)
        {
            GameObject floorObject = Instantiate(_floorSectionPrefab);
            floorObject.transform.SetParent(gameObject.transform);
            FloorSection floorSection = floorObject.GetComponent<FloorSection>();
            Debug.Log("Spatializing floor planes - (" + floorSection.FloorPlanes.Count + ")");
            foreach (FloorPlane plane in floorSection.FloorPlanes)
            {
                plane.SetParameters(floor);
                Debug.Log("Parameters set");
                plane.Spatialize();
                plane.gameObject.AddComponent<MeshCollider>();
                Debug.Log("Spatialized!!!");
            }
        }
        foreach(StoreyPointsCollector.ConnectorPoint connectorPoint in _storeyPointsCollector.ListConnectorPoints())
        {
            GameObject corner = Instantiate(cornerFinishingPointPrefab);
            corner.transform.SetParent(gameObject.transform);
            corner.transform.localScale = new Vector3(30, storey.Height, 30);
            corner.transform.localPosition = new Vector3(connectorPoint.Point.x,storey.Elevation,connectorPoint.Point.y) - transform.localPosition;
            //corner.GetComponentInChildren<WallsConnector>().adjuster.enabled = true;
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
        Debug.Log("BUILDING COMPLETED, GENERATING STOREYS");
        foreach (Storey storey in building.Storeys)
            GenerateStorey(storey);
    }

    void EraseStoreyDrawings()
    {
        Debug.Log("ERASING DRAWINGS...");
        foreach (Transform _transform in gameObject.GetComponentsInChildren<Transform>())
        {
            if(_transform != transform && _transform.parent == transform && _transform != _playerTransform && _transform != _enviornmentTransform)
            {
                Destroy(_transform.gameObject);
            }
        }
        Debug.Log("ERASING DRAWINGS COMPLETED");
    }
}
