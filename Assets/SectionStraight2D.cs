using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionStraight2D : WallSection2D
{
    [SerializeField] protected RectTransform _topLine;
    [SerializeField] protected RectTransform _bottomLine;
    [SerializeField] protected RectTransform _startLine;
    [SerializeField] protected RectTransform _endLine;

    [SerializeField] Transform _start;
    [SerializeField] Transform _end;
    [SerializeField] Transform _top;
    [SerializeField] Transform _bottom;

    float _scaleFactor = 1f;

    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;
        transform.localPosition = WallSection.StartPoint.Position;

        _start.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
        _end.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
        _end.localPosition = new Vector3(Lenght * _scaleFactor, 0f, 0f);

        _top.localPosition = new Vector3(0f, Thickness * _scaleFactor / 2f, 0f);
        _top.localScale = new Vector3(Lenght, 1f, 1f) * _scaleFactor;

        _bottom.localPosition = new Vector3(0f, -Thickness * _scaleFactor / 2f, 0f);
        _bottom.localScale = new Vector3(Lenght * _scaleFactor, 1f, 1f);

        transform.Rotate(-transform.forward, MathHelpers.VectorAzimuthDeg(WallSection.EndPoint.Position - WallSection.StartPoint.Position));

        /*base.DrawOnCanvas(section);

        Vector3 shortsVector = new Vector3(1f/(20f * _scalableSection.localScale.x), _startLine.localScale.y, _startLine.localScale.z);
        Vector3 longsVector = new Vector3(_topLine.localScale.x, _topLine.localScale.y, _topLine.localScale.z);
        _startLine.localScale = shortsVector;
        _endLine.localScale = shortsVector;
        _topLine.localScale = longsVector;
        _bottomLine.localScale = longsVector;*/
    }
}
