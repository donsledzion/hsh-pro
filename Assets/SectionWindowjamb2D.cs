using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionWindowjamb2D : SectionStraight2D
{
    [SerializeField] RectTransform _startLineOffset;
    [SerializeField] RectTransform _endLineOffset;
    [SerializeField] RectTransform _glassLines;

    public override void DrawOnCanvas(WallSection section)
    {
        base.DrawOnCanvas(section);

        Debug.Log("Before: _startLineOffset.anchoredPosition.x: " + _startLineOffset.anchoredPosition.x);
        _startLineOffset.anchoredPosition = Vector2.right * (_startLineOffset.anchoredPosition.x/(_scalableSection.localScale.x*10f));
        
        
        _startLineOffset.localScale = new Vector3(_startLine.localScale.x, _startLineOffset.localScale.y, _startLineOffset.localScale.z);
        //_startLineOffset.localScale = new Vector3(_startLine.localScale.x,_startLineOffset.localScale.y, _startLineOffset.localScale.z);
        _endLineOffset.localScale = new Vector3(_endLine.localScale.x, _endLineOffset.localScale.y, _endLineOffset.localScale.z);
        _endLineOffset.anchoredPosition = _endLine.anchoredPosition + Vector2.left * (_startLineOffset.anchoredPosition.x/*/(_scalableSection.localScale.x*10f)*/);
        Debug.Log("After: _startLineOffset.anchoredPosition.x: " + _startLineOffset.anchoredPosition.x);
        _glassLines.localScale = new Vector3(((WallSection.EndPoint.Position - WallSection.StartPoint.Position).magnitude),_glassLines.localScale.y, _glassLines.localScale.z);
    }
}
