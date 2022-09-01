using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WindowParameters : MonoBehaviour
{
    public float Elevation { get; private set; }
    public float Height { get; private set; }
    public float Width { get; private set; }
    public float Length { get; private set; }
    public float Azimuth { get; private set; }

    public float Windowsill { get; private set; }
    public Vector3 Position { get; private set; }

    public float WallHeight { get; private set; }
    public void SetParameters(Storey storey)
    {
        Elevation = storey.Elevation;
        WallHeight = storey.Height;
        Position = new Vector3(Position.x, storey.Elevation+WallHeight, Position.z);
    }

    public void SetParameters(Windowjamb jamb)
    {
        Length = (jamb.EndPoint.Position - jamb.StartPoint.Position).magnitude;
        Azimuth = MathHelpers.VectorAzimuthDeg(jamb.EndPoint.Position - jamb.StartPoint.Position);
        Position = new Vector3(jamb.StartPoint.Position.x, Position.y, jamb.StartPoint.Position.y);
        Height = jamb.Height;
        Windowsill = jamb.WindowSill;
    }

    public void SetParameters(Wall wall)
    {
        Width = wall.WallType == WallType.LoadBearing ? DefaultSettings.ins.LoadBareringWallWidth : DefaultSettings.ins.PartialWallWidth;
    }

    public void SetParameters(Storey storey, Wall wall, Windowjamb jamb)
    {
        SetParameters(storey);
        SetParameters(wall);
        SetParameters(jamb);
    }
}
