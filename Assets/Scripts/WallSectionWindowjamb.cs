using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionWindowjamb : WallSectionAlt
{
    [SerializeField] Transform _scallableBottomChild;
    new WindowParameters parameters = new WindowParameters();
    public void Spatialize(Windowjamb jamb)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);

        _scallableChild.localScale = new Vector3(parameters.Length, parameters.WallHeight - parameters.Height - parameters.Windowsill, parameters.Width);

        _scallableBottomChild.localScale = new Vector3(parameters.Length,parameters.Windowsill, parameters.Width);
        _phantomTransform.localScale = new Vector3(parameters.Length, parameters.Height, parameters.Width);

        _scallableBottomChild.localPosition = new Vector3(_scallableBottomChild.localPosition.x, -parameters.WallHeight , _scallableBottomChild.localPosition.z);
        _phantomTransform.localPosition = new Vector3(
            _scallableBottomChild.localPosition.x,
            -parameters.WallHeight + parameters.Windowsill, 
            _scallableBottomChild.localPosition.z);

        transform.position = parameters.Position;

        SetTilling();
    }


    public void SetParameters(Storey storey, Wall wall, Windowjamb jamb)
    {
        parameters.SetParameters(storey, wall, jamb);
    }
}
