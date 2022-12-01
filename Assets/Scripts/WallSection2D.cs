using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Walls2D;

public class WallSection2D : MonoBehaviour
{
    [SerializeField] protected Transform _start;
    [SerializeField] protected Transform _end;
    [SerializeField] protected Transform _top;
    [SerializeField] protected Transform _bottom;
    //=========================================================================================
    [SerializeField] protected TextMeshProUGUI _orderInWall;
    //========================================================================================= 

    protected float _scaleFactor = 1f;

    Vector2 _startPoint;
    Vector2 _endPoint;
    protected WallSection _wallSection;

    public Vector2 StartPoint { get { return _startPoint; } }
    public Vector2 EndPoint { get { return _endPoint; } }    
    public WallSection WallSection { get { return _wallSection; } }
    public float Thickness => (WallSection.Wall.WallType == WallType.LoadBearing ? DefaultSettings.ins.LoadBareringWallWidth : DefaultSettings.ins.PartialWallWidth);
    public float Lenght => (WallSection.EndPoint.Position - WallSection.StartPoint.Position).magnitude;

    public virtual void DrawOnCanvas(WallSection section)
    {
        transform.localPosition = WallSection.StartPoint.Position;
        if (GameManager.ins.WallOrderDebugMode == true)
            DrawWallOrderDebugLabels();
        transform.Rotate(-transform.forward, MathHelpers.VectorAzimuthDeg(WallSection.EndPoint.Position - WallSection.StartPoint.Position));
    }

    internal void DrawWallOrderDebugLabels()
    {
        _orderInWall.transform.localPosition = (_end.localPosition + _start.localPosition) / 2f;
        _orderInWall.text = "-> " + WallSection.Wall.OrderInStorey.ToString() + " - " + WallSection.OrderInWall.ToString();
    }


}
