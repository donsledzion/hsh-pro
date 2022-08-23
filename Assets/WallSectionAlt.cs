using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionAlt : MonoBehaviour
{
    WallParameters parameters = new WallParameters();
    [SerializeField] TillingAdjuster tillingAdjuster;

    public void Spatialize(WallSection wallSection)
    {
        transform.localScale = new Vector3(parameters.Length,parameters.Height,parameters.Width);
        transform.position = parameters.Position;
        transform.RotateAround(transform.position,Vector3.up,parameters.Azimuth);

        Vector2 textureTilling = new Vector2(parameters.Length,parameters.Height);
        tillingAdjuster.SetTilling(textureTilling);
    }

    public void SetParameters(Storey storey, Wall wall, WallSection wallSection)
    {
        parameters.SetParameters(storey, wall, wallSection);
    }
}
