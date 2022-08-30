using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionJamb : WallSectionAlt
{
    new DoorParameters parameters = new DoorParameters();
    public void Spatialize(Jamb jamb)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);
        _scallableChild.localScale = new Vector3(parameters.Length, parameters.WallHeight - parameters.Height, parameters.Width);
        transform.position = parameters.Position;

        SetTilling();
    }


    public void SetParameters(Storey storey, Wall wall, Jamb jamb)
    {
        parameters.SetParameters(storey, wall, jamb);
    }
}
