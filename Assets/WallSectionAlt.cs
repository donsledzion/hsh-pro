using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionAlt : MonoBehaviour
{
    protected WallParameters parameters = new WallParameters();
    WallSection _section;
    [SerializeField] protected Transform _scallableChild;
    [SerializeField] protected Transform _phantomTransform;
    [SerializeField] List<TillingAdjuster> tillingAdjustersTop = new List<TillingAdjuster>();
    [SerializeField] List<TillingAdjuster> tillingAdjustersHead = new List<TillingAdjuster>();
    [SerializeField] List<TillingAdjuster> tillingAdjustersFace = new List<TillingAdjuster>();
    
    public WallSection Section { get { return _section; } set { _section = value; } }

    public WallParameters Parameters{ get { return parameters; } }

    public virtual void Spatialize(WallSection wallSection)
    {
        transform.RotateAround(transform.position, Vector3.up, parameters.Azimuth);
        _scallableChild.localScale = new Vector3(parameters.Length, parameters.Height, parameters.Width);
        transform.position = parameters.Position;
        SetTilling();
    }

    public void SetParameters(Storey storey, Wall wall, WallSection wallSection)
    {
        parameters.SetParameters(storey, wall, wallSection);
        Section = wallSection;
    }

    public void SetTilling()
    {
        foreach(TillingAdjuster adjuster in tillingAdjustersTop)
        {
            Vector2 textureScale = new Vector2(_scallableChild.localScale.x* parameters.Azimuth,parameters.Length);
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
}
