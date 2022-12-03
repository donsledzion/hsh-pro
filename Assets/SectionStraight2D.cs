using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Walls2D;

public class SectionStraight2D : WallSection2D
{
    [SerializeField] private Transform _topMidToStart;
    [SerializeField] private Transform _topMidToEnd;
    [SerializeField] private Transform _bottomMidToStart;
    [SerializeField] private Transform _bottomMidToEnd;

    Color debugSingleEnded = Color.black;
    Color debugDoubleEndedInner = Color.red;
    Color debugDoubleEndedOuter = Color.blue;
    Color debugDoubleStraight = Color.green;

    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;
        HandleEndings();
        HandleHorizontalLines();
        base.DrawOnCanvas(section);
    }

    private void HandleHorizontalLines()
    {
        HandleTopLine();
        HandleBottomLine();
    }

    private void HandleTopLine()
    {
        _topMidToStart.localPosition = new Vector3(Lenght/2f, Thickness * _scaleFactor / 2f, 0f);
        _topMidToStart.localScale = new Vector3(-Lenght/2, 1f, 1f) * _scaleFactor; //(need to calculate the correct length of this line -> depending on intersection with neighbouring section!
        _topMidToEnd.localPosition = new Vector3(Lenght/2f, Thickness * _scaleFactor / 2f, 0f);
        _topMidToEnd.localScale = new Vector3(Lenght/2, 1f, 1f) * _scaleFactor; //(need to calculate the correct length of this line -> depending on intersection with neighbouring section!
    }
    private void HandleBottomLine()
    {
        _bottomMidToStart.localPosition =  new Vector3(Lenght / 2f, -Thickness * _scaleFactor / 2f, 0f);
        _bottomMidToStart.localScale =     new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor; //(need to calculate the correct length of this line -> depending on intersection with neighbouring section!
        _bottomMidToEnd.localPosition =    new Vector3(Lenght / 2f, -Thickness * _scaleFactor / 2f, 0f);
        _bottomMidToEnd.localScale =       new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor; //(need to calculate the correct length of this line -> depending on intersection with neighbouring section!
    }

    private void HandleEndings()
    {
        HandleStart();
        HandleEnd();
        
    }

    private void HandleStart()
    {
        List<BasePoint> pointsAtStartPosition = CurrentStorey.BasePointsAtPosition(StartPoint, typeof(SectionStraight));
        if (pointsAtStartPosition.Count == 1)
            _start.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
        else if(pointsAtStartPosition.Count == 2)
        {
            WallSection otherSection = null;
            foreach(BasePoint point in pointsAtStartPosition)
            {
                if (point.WallSection != null && point.WallSection != this.WallSection)
                    otherSection = point.WallSection;
            }
            if(otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
            }

        }

    }

    private void HandleEnd()
    {
        List<BasePoint> pointsAtEndPosition = CurrentStorey.BasePointsAtPosition(EndPoint, typeof(SectionStraight));
        if(pointsAtEndPosition.Count == 1)
        {
            _end.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
            _end.localPosition = new Vector3(Lenght * _scaleFactor, 0f, 0f);
        }
        else if(pointsAtEndPosition.Count == 2)
        {

        }
    }
}
