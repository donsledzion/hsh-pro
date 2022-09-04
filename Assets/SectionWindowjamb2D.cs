using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionWindowjamb2D : SectionStraight2D
{
    [SerializeField] RectTransform _startLineOffset;
    [SerializeField] RectTransform _endLineOffset;

    public override void DrawOnCanvas(WallSection section)
    {
        base.DrawOnCanvas(section);

        _startLineOffset.localScale = new Vector3(_startLine.localScale.x,_startLineOffset.localScale.y, _startLineOffset.localScale.z);
        _endLineOffset.localScale = new Vector3(_endLine.localScale.x, _endLineOffset.localScale.y, _endLineOffset.localScale.z);
    }
}
