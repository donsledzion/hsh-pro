using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionStraight2D : WallSection2D
{
    [SerializeField] RectTransform _topLine;
    [SerializeField] RectTransform _bottomLine;
    [SerializeField] RectTransform _startLine;
    [SerializeField] RectTransform _endLine;

    public override void DrawOnCanvas(WallSection section)
    {
        base.DrawOnCanvas(section);

        Vector3 shortsVector = new Vector3(1f / ((WallSection.EndPoint.Position - WallSection.StartPoint.Position).magnitude / 120f), _startLine.localScale.y, _startLine.localScale.z);
        Vector3 longsVector = new Vector3(_topLine.localScale.x,1f/(WallSection.Wall.WallType == WallType.LoadBearing ? DefaultSettings.ins.LoadBareringWallWidth / 40f : DefaultSettings.ins.PartialWallWidth / 40f), _topLine.localScale.z);
        _startLine.localScale = shortsVector;
        _endLine.localScale = shortsVector;
        _topLine.localScale = longsVector;
        _bottomLine.localScale = longsVector;
    }
}
