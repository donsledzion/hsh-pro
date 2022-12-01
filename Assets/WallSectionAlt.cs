using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionAlt : MonoBehaviour
{
    protected WallParameters parameters = new WallParameters();
    WallSection _section;
    [SerializeField] protected Transform _scallableChild;
    [SerializeField] protected List<TillingAdjuster> tillingAdjustersTop = new List<TillingAdjuster>();
    [SerializeField] protected List<TillingAdjuster> tillingAdjustersHead = new List<TillingAdjuster>();
    [SerializeField] protected List<TillingAdjuster> tillingAdjustersFace = new List<TillingAdjuster>();


    public WallSection Section { get { return _section; } set { _section = value; } }

    public WallParameters Parameters{ get { return parameters; } }

    public virtual void Spatialize(WallSection wallSection)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);
        _scallableChild.localScale = new Vector3(parameters.Length, parameters.Height, parameters.Width);
        transform.position = parameters.Position;
        SetTilling();
    }

    public virtual void SetParameters(Storey storey, Wall wall, WallSection wallSection)
    {
        parameters.SetParameters(storey, wall, wallSection);
        Section = wallSection;
    }

    public virtual void SetTilling()
    {
        foreach(TillingAdjuster adjuster in tillingAdjustersTop)
        {
            Vector2 textureScale = new Vector2(_scallableChild.localScale.x, _scallableChild.localScale.z);
            adjuster.SetTilling(textureScale);
        }
        foreach(TillingAdjuster adjuster in tillingAdjustersHead)
        {
            Vector2 textureScale = new Vector2(_scallableChild.localScale.z,_scallableChild.localScale.y);
            adjuster.SetTilling(textureScale);
        }
        foreach(TillingAdjuster adjuster in tillingAdjustersFace)
        {
            Vector2 textureScale = new Vector2(_scallableChild.localScale.x, _scallableChild.localScale.y);
            adjuster.SetTilling(textureScale);
        }
    }

    public void ApplyPaintings()
    {
        foreach(TillingAdjuster plane in tillingAdjustersHead)
        {
            Material assignedMaterial = PlaneNameToMaterial(Section, plane.gameObject.name);
            if (assignedMaterial != null)
                plane.GetComponent<MeshRenderer>().material = assignedMaterial;
        }
        foreach(TillingAdjuster plane in tillingAdjustersTop)
        {
            Material assignedMaterial = PlaneNameToMaterial(Section, plane.gameObject.name);
            if (assignedMaterial != null)
                plane.GetComponent<MeshRenderer>().material = assignedMaterial;
        }
        foreach(TillingAdjuster plane in tillingAdjustersFace)
        {
            Material assignedMaterial = PlaneNameToMaterial(Section, plane.gameObject.name);
            if (assignedMaterial != null)
                plane.GetComponent<MeshRenderer>().material = assignedMaterial;
        }
    }

    protected Material GetMaterialByAssetName(string assetName, string planeName)
    {
        if (assetName == "wrong-plane-name")
        {
            Debug.LogWarning("Material not loaded! Can't find proper plane by name! Plane name: " + planeName);
            return null;
        }
        if (assetName == "" || assetName == null)
        {
            Debug.LogWarning("Material not loaded! Plane name null or empty: " + planeName);
            return null;
        }
        AssetBundle bundle = AssetBundleLoader.ins.WallSurfacesBundle.LoadBundle();
        if (bundle == null)
        {
            Debug.LogError("Bundle not loaded! Can't assign wall material! Plane name: " + planeName);
            return null;
        }
        ScriptableObjectsController item = bundle.LoadAsset(assetName) as ScriptableObjectsController;
        return item.material;
    }

    protected virtual Material PlaneNameToMaterial(WallSection section, string planeName)
    {
        Debug.Log("Looking for plane named: " + planeName);
        string assetName = "";
        StraightSectionPaintingSetup paintingSetup = section.PaintingSetup as StraightSectionPaintingSetup;
        switch (planeName)
        {            
            case "PlaneHeadStartLeft":
                assetName = paintingSetup.AHeadPaintingA;
                break;
            case "PlaneHeadStartRight":
                assetName = paintingSetup.AHeadPaintingB;
                break;
            case "PlaneHeadEndLeft":
                assetName = paintingSetup.BHeadPaintingA;
                break;
            case "PlaneHeadEndRight":
                assetName = paintingSetup.BHeadPaintingB;
                break;
            case "PlaneFaceLeft":
                assetName = paintingSetup.AFacePainting;
                break;
            case "PlaneFaceRight":
                assetName = paintingSetup.BFacePainting;
                break;
            default:
                assetName = "wrong-plane-name";
                break;
        }
        return GetMaterialByAssetName(assetName, planeName);
    }
    
}
