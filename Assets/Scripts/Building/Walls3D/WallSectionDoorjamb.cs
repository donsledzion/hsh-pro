using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionDoorjamb : WallSectionJamb
{
    new DoorParameters parameters = new DoorParameters();
    public void Spatialize(Doorjamb jamb)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);
        _scallableChild.localScale = new Vector3(parameters.Length, parameters.WallHeight - parameters.Height, parameters.Width);
        _phantomScaler.transform.localScale = new Vector3(parameters.Length, parameters.Height, parameters.Width);
        transform.position = parameters.Position;
        _phantomScaler.transform.localPosition = new Vector3(
            _scallableChild.localPosition.x,
            - (parameters.WallHeight - parameters.Height), 
            _scallableChild.localPosition.x);

        SetTilling();
    }


    public void SetParameters(Storey storey, Wall wall, Doorjamb jamb)
    {
        parameters.SetParameters(storey, wall, jamb);
        Section = jamb;
    }

    protected override Material PlaneNameToMaterial(WallSection section, string planeName)
    {
        Debug.Log("Looking for plane named: " + planeName);
        string assetName = "";
        DoorjambSectionPaintingSetup paintingSetup = section.PaintingSetup as DoorjambSectionPaintingSetup;

        switch (planeName)
        {
            case "PlaneFaceLeft":
                assetName = paintingSetup.AFacing;
                break;
            case "PlaneFaceRight":
                assetName = paintingSetup.BFacing;
                break;
            case "PlaneBottomLeft":
                assetName = paintingSetup.ABottom;
                break;
            case "PlaneBottomRight":
                assetName = paintingSetup.BBottom;
                break;            
            default:
                assetName = "wrong-plane-name";
                break;
        }
        return GetMaterialByAssetName(assetName, planeName);
    }

}
