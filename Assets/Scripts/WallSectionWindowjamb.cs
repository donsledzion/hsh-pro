using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionWindowjamb : WallSectionAlt
{
    [SerializeField] GameObject _scallableBottomChild;
    new WindowParameters parameters = new WindowParameters();
    public void Spatialize(Windowjamb jamb)
    {
/*
 * TODO - create a prefab of window and scale it properly.
 */
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);
        _scallableChild.localScale = new Vector3(parameters.Length, parameters.WallHeight - parameters.Height, parameters.Width);
        transform.position = parameters.Position;

        SetTilling();
    }


    public void SetParameters(Storey storey, Wall wall, Windowjamb jamb)
    {
        parameters.SetParameters(storey, wall, jamb);
    }
}
