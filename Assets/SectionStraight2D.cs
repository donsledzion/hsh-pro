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

    public override void DrawOnCanvas(WallSection section)
    {
        base.DrawOnCanvas(section);

        Vector3 shortsVector = new Vector3(1f/(20f * _scalableSection.localScale.x), _startLine.localScale.y, _startLine.localScale.z);
        Vector3 longsVector = new Vector3(_topLine.localScale.x, _topLine.localScale.y, _topLine.localScale.z);
        _startLine.localScale = shortsVector;
        _endLine.localScale = shortsVector;
        _topLine.localScale = longsVector;
        _bottomLine.localScale = longsVector;
    }
}
