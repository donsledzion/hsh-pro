using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallParameters
{
    public float Elevation { get; private set; }
    public float Height { get; private set; }
    public float Width { get; private set; }
    public float Length { get; private set; }
    public float Azimuth { get; private set; }
    public Vector3 Position { get; private set; }

    public void SetParameters(Storey storey)
    {
        Elevation = storey.Elevation;
        Height = storey.Height;
        Position = new Vector3(Position.x,storey.Elevation,Position.z);
    }

    public void SetParameters(WallSection wallSection)
    {
        Length = (wallSection.EndPoint.Position - wallSection.StartPoint.Position).magnitude;
        Azimuth = MathHelpers.VectorAzimuthDeg(wallSection.EndPoint.Position - wallSection.StartPoint.Position);
        Position = new Vector3(wallSection.StartPoint.Position.x,Position.y,wallSection.StartPoint.Position.y);
    }

    public void SetParameters(Wall wall)
    {
        Width = wall.WallType == WallType.LoadBearing ? DefaultSettings.ins.LoadBareringWallWidth : DefaultSettings.ins.PartialWallWidth;
    }

    public void SetParameters(Storey storey, Wall wall, WallSection wallSection)
    {
        SetParameters(storey);
        SetParameters(wall);
        SetParameters(wallSection);
    }
}
