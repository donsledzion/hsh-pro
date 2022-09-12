using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionDoorjamb : WallSectionAlt
{
    new DoorParameters parameters = new DoorParameters();
    public void Spatialize(Doorjamb jamb)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);
        _scallableChild.localScale = new Vector3(parameters.Length, parameters.WallHeight - parameters.Height, parameters.Width);
        _phantomTransform.localScale = new Vector3(parameters.Length, parameters.Height, parameters.Width);
        transform.position = parameters.Position;
        _phantomTransform.position = new Vector3(parameters.Position.x,parameters.Height, parameters.Position.z);

        SetTilling();
    }


    public void SetParameters(Storey storey, Wall wall, Doorjamb jamb)
    {
        parameters.SetParameters(storey, wall, jamb);
    }
}
