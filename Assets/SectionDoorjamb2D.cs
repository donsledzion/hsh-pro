using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class SectionDoorjamb2D : WallSection2D
{
    [SerializeField] RectTransform _archImageSpriteLeft;
    [SerializeField] RectTransform _archImageSpriteRight;
    [SerializeField] float _archImageRatio = .91f;
    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;

        _archImageSpriteLeft.localScale = new Vector3(_archImageRatio*Lenght, _archImageRatio * Lenght, 1f)/100f;
        _end.localPosition = new Vector3(Lenght * _scaleFactor, 0f, 0f);

        base.DrawOnCanvas(section);
    }
}
