using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionWindowjamb : WallSectionJamb
{
    [SerializeField] Transform _scallableBottomChild;

    new WindowParameters parameters = new WindowParameters();
    public void Spatialize(Windowjamb jamb)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);

        _scallableChild.localScale = new Vector3(parameters.Length, parameters.WallHeight - parameters.Height - parameters.Windowsill, parameters.Width);

        _scallableBottomChild.localScale = new Vector3(parameters.Length,parameters.Windowsill, parameters.Width);
        _phantomScaler.transform.localScale = new Vector3(parameters.Length, parameters.Height, parameters.Width);

        _scallableBottomChild.localPosition = new Vector3(_scallableBottomChild.localPosition.x, -parameters.WallHeight , _scallableBottomChild.localPosition.z);
        _phantomScaler.transform.localPosition = new Vector3(
            _scallableBottomChild.localPosition.x,
            -parameters.WallHeight + parameters.Windowsill, 
            _scallableBottomChild.localPosition.z);

        transform.position = parameters.Position;

        SetTilling();
    }


    public void SetParameters(Storey storey, Wall wall, Windowjamb jamb)
    {
        parameters.SetParameters(storey, wall, jamb);
        Section = jamb;
    }

    public override void SetTilling()
    {
        base.SetTilling();

        foreach (TillingAdjuster adjuster in bottomTillingAdjustersFace)
        {
            //Debug.LogWarning("Trying to paint bottoms window sections");
            Vector2 textureScale = new Vector2(_scallableBottomChild.localScale.x, _scallableBottomChild.localScale.y);
            adjuster.SetTilling(textureScale);
        }
    }


    protected override Material PlaneNameToMaterial(WallSection section, string planeName)
    {
        //Debug.Log("Looking for plane named: " + planeName);
        string assetName = "";
        WindowjambSectionPaintingSetup paintingSetup = section.PaintingSetup as WindowjambSectionPaintingSetup;

        switch (planeName)
        {
            case "UpperPlaneFaceLeft":
                assetName = paintingSetup.AUpperFacing;
                break;
            case "UpperPlaneFaceRight":
                assetName = paintingSetup.BUpperFacing;
                break;
            case "UpperPlaneBottomLeft":
                assetName = paintingSetup.AUpperBottom;
                break;
            case "UpperPlaneBottomRight":
                assetName = paintingSetup.BUpperBottom;
                break;
            case "LowerPlaneFaceLeft":
                assetName = paintingSetup.ALowerFacing;
                break;
            case "LowerPlaneFaceRight":
                assetName = paintingSetup.BLowerFacing;
                break;
            case "LowerPlaneTopLeft":
                assetName = paintingSetup.ALowerTop;
                break;
            case "LowerPlaneTopRight":
                assetName = paintingSetup.BLowerTop;
                break;
            default:
                assetName = "wrong-plane-name";
                break;
        }
        return GetMaterialByAssetName(assetName, planeName);
    }
}
