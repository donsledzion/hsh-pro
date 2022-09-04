using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSection2D : MonoBehaviour
{
    Vector2 _startPoint;
    Vector2 _endPoint;
    float _thickness;
    WallSection _wallSection;
    [SerializeField] protected Transform _scalableSection;

    public Vector2 StartPoint { get { return _startPoint; } }
    public Vector2 EndPoint { get { return _endPoint; } }
    public float Thickness { get { return _thickness; } }
    public WallSection WallSection { get { return _wallSection; } }

    public virtual void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;
        Debug.Log("Wall Section: " + section);
        Debug.Log("Wall type: " + section.Wall.WallType);
        _scalableSection.localScale = new Vector3((WallSection.EndPoint.Position - WallSection.StartPoint.Position).magnitude/600f
            ,(WallSection.Wall.WallType == WallType.LoadBearing ? DefaultSettings.ins.LoadBareringWallWidth : DefaultSettings.ins.PartialWallWidth)/600f
            , 0f);
        transform.localPosition = WallSection.StartPoint.Position;
        transform.Rotate(-transform.forward, MathHelpers.VectorAzimuthDeg(WallSection.EndPoint.Position - WallSection.StartPoint.Position));
    }


}
