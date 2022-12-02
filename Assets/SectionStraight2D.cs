using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Walls2D;

public class SectionStraight2D : WallSection2D
{

    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;

        
        HandleEndings();

        _top.localPosition = new Vector3(0f, Thickness * _scaleFactor / 2f, 0f);
        _top.localScale = new Vector3(Lenght, 1f, 1f) * _scaleFactor;

        _bottom.localPosition = new Vector3(0f, -Thickness * _scaleFactor / 2f, 0f);
        _bottom.localScale = new Vector3(Lenght * _scaleFactor, 1f, 1f);

        base.DrawOnCanvas(section);
    }

    private void HandleEndings()
    {
        HandleStart();
        HandleEnd();
        
    }

    private void HandleStart()
    {
        _start.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
    }

    private void HandleEnd()
    {
        _end.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
        _end.localPosition = new Vector3(Lenght * _scaleFactor, 0f, 0f);
    }
}
