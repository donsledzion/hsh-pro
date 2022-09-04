using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionWindowjamb2D : SectionStraight2D
{
    [SerializeField] RectTransform _startLineOffset;
    [SerializeField] RectTransform _endLineOffset;
    [SerializeField] RectTransform _glassLines;

    [SerializeField] Transform _start;
    [SerializeField] Transform _end;
    [SerializeField] Transform _top;
    [SerializeField] Transform _bottom;

    float _scaleFactor = 1f;

    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;
        transform.localPosition = WallSection.StartPoint.Position;

        _start.localScale = new Vector3(1f, Thickness*_scaleFactor, 1f);
        _end.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
        _end.localPosition = new Vector3(Lenght*_scaleFactor,0f,0f);

        _top.localPosition = new Vector3(0f, Thickness * _scaleFactor/2f, 0f);
        _top.localScale = new Vector3(Lenght,1f,1f) * _scaleFactor;

        _bottom.localPosition = new Vector3(0f, -Thickness * _scaleFactor/2f, 0f);
        _bottom.localScale = new Vector3(Lenght * _scaleFactor, 1f, 1f);

        transform.Rotate(-transform.forward, MathHelpers.VectorAzimuthDeg(WallSection.EndPoint.Position - WallSection.StartPoint.Position));


        /*base.DrawOnCanvas(section);

        Debug.Log("Before: _startLineOffset.anchoredPosition.x: " + _startLineOffset.anchoredPosition.x);
        _startLineOffset.anchoredPosition = Vector2.right * (_startLineOffset.anchoredPosition.x/(_scalableSection.localScale.x*10f));
        
        
        _startLineOffset.localScale = new Vector3(_startLine.localScale.x, _startLineOffset.localScale.y, _startLineOffset.localScale.z);
        //_startLineOffset.localScale = new Vector3(_startLine.localScale.x,_startLineOffset.localScale.y, _startLineOffset.localScale.z);
        _endLineOffset.localScale = new Vector3(_endLine.localScale.x, _endLineOffset.localScale.y, _endLineOffset.localScale.z);
        _endLineOffset.anchoredPosition = _endLine.anchoredPosition + Vector2.left * (_startLineOffset.anchoredPosition.x*//*/(_scalableSection.localScale.x*10f)*//*);
        Debug.Log("After: _startLineOffset.anchoredPosition.x: " + _startLineOffset.anchoredPosition.x);
        _glassLines.localScale = new Vector3(((WallSection.EndPoint.Position - WallSection.StartPoint.Position).magnitude),_glassLines.localScale.y, _glassLines.localScale.z);*/
    }
}
