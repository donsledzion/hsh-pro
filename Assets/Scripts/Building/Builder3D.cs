using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Valve.VR.InteractionSystem;
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
                    sectionAlt.ApplyPaintings();
                    sectionAlt.SetTilling();
                }
                else if(section is Doorjamb)
                {
                    GameObject sectionObject = Instantiate(_sectionDoorjambPrefab, gameObject.transform);
                    WallSectionDoorjamb sectionJamb = sectionObject.GetComponent<WallSectionDoorjamb>();
                    sectionJamb.SetParameters(storey, wall, (Doorjamb)section);
                    sectionJamb.Spatialize((Doorjamb)section);
                    sectionJamb.InsertJoinery();
                    sectionJamb.ApplyPaintings();
                    sectionJamb.SetTilling();
                }
                else if(section is Windowjamb)
                {
                    GameObject sectionObject = Instantiate(_sectionWindowjambPrefab, gameObject.transform);
                    WallSectionWindowjamb sectionJamb = sectionObject.GetComponent<WallSectionWindowjamb>();
                    sectionJamb.SetParameters(storey, wall, (Windowjamb)section);
                    sectionJamb.Spatialize((Windowjamb)section);
                    sectionJamb.InsertJoinery();
                    sectionJamb.ApplyPaintings();
                    sectionJamb.SetTilling();
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
                plane.DrawMaterial();
                plane.gameObject.AddComponent<MeshCollider>();
            }
        }
        // Inserting "corners" (rounded so far) on wall connections
        foreach(StoreyPointsCollector.ConnectorPoint connectorPoint in _storeyPointsCollector.ListConnectorPoints(storey))
        {
            GameObject corner = Instantiate(cornerFinishingPointPrefab);
            corner.transform.SetParent(gameObject.transform);
            float size = connectorPoint.ThickestWallThickness();
            WallsConnector connector = corner.GetComponent<WallsConnector>();
            connector.Sections = connectorPoint.sections;
            corner.transform.localScale = new Vector3(size, storey.Height, size);
            corner.transform.localPosition = new Vector3(connectorPoint.Point.x,storey.Elevation,connectorPoint.Point.y) - transform.localPosition;
            connector.DelayedMaterialAssigning();
        }

        foreach(Equipment equipment in storey.Equipment)
        {
            SpawnEquipment(equipment);
        }
        
    }

    private void SpawnEquipment(Equipment equipment)
    {
        if (equipment == null) return;
        if (equipment.AssetName == null || equipment.AssetName == "") return;
        if (equipment.BundleName == null || equipment.BundleName== "") return;
        AssetBundle bundle = AssetBundleLoader.ins.GetBundleLoadStatusByName(equipment.BundleName).Bundle;
        ScriptableObjectsController item = bundle.LoadAsset(equipment.AssetName) as ScriptableObjectsController;
        GameObject equipmentInstance = Instantiate(item.prefab);
        equipmentInstance.transform.SetParent(gameObject.transform);
        equipmentInstance.transform.position = equipment.Position;
        equipmentInstance.transform.eulerAngles = equipment.Rotation;
        EquipmentItem equipmentItem = equipmentInstance.GetComponent<EquipmentItem>();
        equipmentItem.GUID = equipment.GUID;
        equipmentItem.Equipment = equipment;
        equipmentItem.enabled = false;
        Throwable throwable = equipmentItem.GetComponent<Throwable>();
        if (throwable != null)
        {
            throwable.restoreOriginalParent = true;
            Rigidbody rb = equipmentItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        //Debug.Log("Equipment item " + equipmentItem.GUID + " has been spawned with equipment model: " + equipment.ToString());
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
        GenerateFlooring();
    }

    void GenerateFlooring()
    {
        if (GameManager.ins.Building.Storeys[0].Walls.Count > 0)
        {
            Vector2[] flooringPoints = PolygonHelper.RangeToRect(
                PolygonHelper.PlaneRange(
                    _storeyPointsCollector.WallPointsListToVectorArray(
                        _storeyPointsCollector.CollectAllPoints(
                            GameManager.ins.Building.Storeys[0]))), 50f);
            GameObject flooringObject = Instantiate(_ceilingSectionPrefab);
            flooringObject.transform.SetParent(gameObject.transform);
            CeilingSection flooringSection = flooringObject.GetComponent<CeilingSection>();
            Ceiling flooring = new Ceiling(10f, 0f, flooringPoints);
            CeilingPlane flooringPlane = flooringSection.CeilingPlanes[0];
            flooringPlane.SetParameters(flooring);
            flooringPlane.Spatialize();
        }
    }

    public void EraseStoreyDrawings()
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
