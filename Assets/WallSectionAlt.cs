using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionAlt : MonoBehaviour
{
    WallParameters parameters = new WallParameters();
    [SerializeField] List<TillingAdjuster> tillingAdjustersTop = new List<TillingAdjuster>();
    [SerializeField] List<TillingAdjuster> tillingAdjustersHead = new List<TillingAdjuster>();
    [SerializeField] List<TillingAdjuster> tillingAdjustersFace = new List<TillingAdjuster>();

    public void Spatialize(WallSection wallSection)
    {
        transform.localScale = new Vector3(parameters.Length,parameters.Height,parameters.Width);
        transform.position = parameters.Position;
        transform.RotateAround(transform.position,Vector3.up,parameters.Azimuth);

        SetTilling();
    }

    public void SetParameters(Storey storey, Wall wall, WallSection wallSection)
    {
        parameters.SetParameters(storey, wall, wallSection);
    }

    public void SetTilling()
    {
        foreach(TillingAdjuster adjuster in tillingAdjustersTop)
        {
            Vector2 textureScale = new Vector2(parameters.Width*parameters.Azimuth,parameters.Length);
            adjuster.SetTilling(textureScale);
        }
        foreach(TillingAdjuster adjuster in tillingAdjustersHead)
        {
            Vector2 textureScale = new Vector2(parameters.Width,parameters.Height);
            adjuster.SetTilling(textureScale);
        }
        foreach(TillingAdjuster adjuster in tillingAdjustersFace)
        {
            Vector2 textureScale = new Vector2(parameters.Length,parameters.Height);
            adjuster.SetTilling(textureScale);
        }
    }
}
