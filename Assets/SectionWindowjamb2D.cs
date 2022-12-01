using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionWindowjamb2D : SectionStraight2D
{
    [SerializeField] RectTransform _startLineOffset;
    [SerializeField] RectTransform _glassWrapper;
    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;

        _start.localScale = new Vector3(1f, Thickness*_scaleFactor, 1f);
        _end.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
        _end.localPosition = new Vector3(Lenght*_scaleFactor,0f,0f);

        _top.localPosition = new Vector3(0f, Thickness * _scaleFactor/2f, 0f);
        _top.localScale = new Vector3(Lenght,1f,1f) * _scaleFactor;

        _bottom.localPosition = new Vector3(0f, -Thickness * _scaleFactor/2f, 0f);
        _bottom.localScale = new Vector3(Lenght * _scaleFactor, 1f, 1f);

        _glassWrapper.localScale = new Vector3(Lenght-2*_startLineOffset.localPosition.x,1f,1f);

        base.DrawOnCanvas(section);
    }
}
