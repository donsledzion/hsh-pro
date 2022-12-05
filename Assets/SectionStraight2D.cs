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

    public bool hasBeenDrawn = false;

    [SerializeField] GameObject _cornerMarkerDotPrefab;

    public override void DrawOnCanvas(WallSection section)
    {
        _wallSection = section;
        HandleEndings();
        HandleHorizontalLines();
        base.DrawOnCanvas(section);
    }

    private void HandleHorizontalLines()
    {
        SetupPositions();
        HandleStartSite();
        HandleEndSite();
    }

    private void SetupPositions()
    {
        _topMidToStart.localPosition = new Vector3(Lenght / 2f, Thickness * _scaleFactor / 2f, 0f);
        _bottomMidToStart.localPosition = new Vector3(Lenght / 2f, -Thickness * _scaleFactor / 2f, 0f);
        _topMidToEnd.localPosition = new Vector3(Lenght / 2f, Thickness * _scaleFactor / 2f, 0f);
        _bottomMidToEnd.localPosition = new Vector3(Lenght / 2f, -Thickness * _scaleFactor / 2f, 0f);
    }

    private void HandleStartSite()
    {
        List<BasePoint> pointsAtStartPosition = CurrentStorey.BasePointsAtPosition(StartPoint, typeof(SectionStraight));
        if (pointsAtStartPosition.Count == 2)
        {
            WallSection otherSection = null;
            foreach (BasePoint point in pointsAtStartPosition)
            {
                if (point.WallSection != null && point.WallSection != this.WallSection)
                    otherSection = point.WallSection;
            }
            if (otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
                return;
            }
            //Debug.Log("Start Section: Angle crossing with other section: " + WallSection.AngleBetweenDeg(otherSection).ToString());

            float angDeg = WallSection.AngleBetweenDeg(otherSection);
            float angRad = angDeg * Mathf.PI / 180;
            float angCompDeg = 180f - angDeg;
            float angCompRad = angCompDeg * Mathf.PI / 180;

            Vector2 V1 = new Vector2();
            Vector2 V2 = new Vector2((otherSection.Thickness / 2f) / Mathf.Sin(angCompRad), 0f);

            if ((otherSection.StartPoint.Position - WallSection.StartPoint.Position).magnitude <= 1f)
            {
                V1 = new Vector3(Mathf.Cos(angCompRad), Mathf.Sin(angCompRad), 0f) * (Thickness / 2f) / Mathf.Sin(angCompRad);
            }
            else
            {
                V1 = new Vector3(Mathf.Cos(angCompRad), Mathf.Sin(angCompRad), 0f) * (Thickness / 2f) / Mathf.Sin(angCompRad);
            }

            GameObject innerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);
            GameObject outerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);
            Debug.Log("Angle: " + angCompDeg + "deg (" + angCompRad + "rad , " + angDeg + " angDeg),  V1: " + V1 + " | V2: " + V2);
            if (Mathf.Abs(angDeg - 90f) < 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
                Debug.Log("Angle = 90!");
            }
            else if (Mathf.Abs(angDeg - 270f) < 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                Debug.Log("Angle = 270!");
            }
            else if ((angDeg > 91f && angDeg < 179f))
            {
                innerCornerDot.transform.localPosition = new Vector3(V2.x, V2.y, 0f) +new Vector3(-V1.x, V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(-V2.x, V2.y, 0f) +new Vector3(V1.x, -V1.y, 0f);
                Debug.Log("Start: Option: Angle: " + angDeg + " + assigning!");
            }
            else if ((angDeg > 271f && angDeg < 359f))//==================================== working on it =====================
            {
                innerCornerDot.transform.localPosition = new Vector3(V2.x, V2.y, 0f) +new Vector3(V1.x, V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(-V2.x, V2.y, 0f) +new Vector3(-V1.x, -V1.y, 0f);
                Debug.Log("START: Option: Angle: " + angDeg + " + assigning!");
            }
            innerCornerDot.transform.localScale = Vector3.one * 2f;
            innerCornerDot.GetComponent<Image>().color = Color.red;
            outerCornerDot.transform.localScale = Vector3.one * 2f;
            outerCornerDot.GetComponent<Image>().color = Color.blue;

            if (!GameManager.ins.CornersFinishingDebugMode)
            {
                innerCornerDot.gameObject.SetActive(false);
                outerCornerDot.gameObject.SetActive(false);
            }

            float topDist = outerCornerDot.transform.localPosition.x;
            float botDist = innerCornerDot.transform.localPosition.x;
            Debug.Log("TopDist: " + topDist + " | BotDist: " + botDist);

            if (Mathf.Abs(angDeg - 90f) < 1f)
            {
                _topMidToStart.localScale = new Vector3(-(Lenght / 2 + topDist), 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-(Lenght / 2 + botDist), 1f, 1f) * _scaleFactor;
            }
            else if (Mathf.Abs(angDeg - 270f) < 1f)
            {
                Debug.Log("270 - drawing!");
                _topMidToStart.localScale = new Vector3(-(Lenght / 2 - topDist), 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-(Lenght / 2 - botDist), 1f, 1f) * _scaleFactor;
            }
            else if(angDeg > 91f && angDeg < 179f)//==================================== working on it
            {
                Debug.Log("START: Option: Angle: " + angDeg + " + drawing!");
                _topMidToStart.localScale = new Vector3(-(Lenght + topDist) / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-(Lenght + botDist) / 2, 1f, 1f) * _scaleFactor;
            }
            else if ((angDeg > 271f && angDeg < 359f))
            {
                Debug.Log("START: Option: Angle: " + angDeg + " + drawing!");
                _topMidToStart.localScale = new Vector3(-((Lenght + topDist)) / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-((Lenght + botDist)) / 2, 1f, 1f) * _scaleFactor;
            }
            else
            {
                _topMidToStart.localScale = new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor;
            }
        }
        else
        {
            _topMidToStart.localScale = new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor;
            _bottomMidToStart.localScale = new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor;
        }

    }
    private void HandleEndSite()
    {
        List<BasePoint> pointsAtEndPosition = CurrentStorey.BasePointsAtPosition(EndPoint, typeof(SectionStraight));
        if (pointsAtEndPosition.Count == 2)
        {
            //Debug.Log("HandleEndSite 1");
            WallSection otherSection = null;
            foreach (BasePoint point in pointsAtEndPosition)
            {
                if (point.WallSection != null && point.WallSection != this.WallSection)
                    otherSection = point.WallSection;
            }
            if (otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
                return;
            }
            //Debug.Log("Start Section: Angle crossing with other section: " + WallSection.AngleBetweenDeg(otherSection).ToString());

            float angDeg = WallSection.AngleBetweenDeg(otherSection);
            float angRad = angDeg * Mathf.PI / 180;
            float angCompDeg = 180f - angDeg;
            float angCompRad = angCompDeg * Mathf.PI / 180;

            Vector2 V1 = new Vector2();
            Vector2 V2 = new Vector2((otherSection.Thickness / 2f) / Mathf.Sin(angCompRad),0f);
            if ((otherSection.StartPoint.Position - WallSection.EndPoint.Position).magnitude <= 5f)
            {
                V1 =new Vector3(Mathf.Cos(angCompRad), Mathf.Sin(angCompRad), 0f) * (Thickness / 2f) / Mathf.Sin(angCompRad);
                //Debug.Log("Assigning V1 HERE!!! V1: " + V1);
            }
            else
            {
                V1 = new Vector3(Mathf.Cos(angCompRad), Mathf.Sin(angCompRad), 0f) * (Thickness / 2f) / Mathf.Sin(angCompRad);
                //Debug.Log("Assigning V1 IN ELSE!!!");
            }
            GameObject innerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);
            GameObject outerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);

            if (Mathf.Abs(angDeg - 90f) < 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
            }
            else if (Mathf.Abs(angDeg - 270f) < 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                Debug.Log("END: Option: Angle: " + angDeg + " + assigning!");
            }
            else if (angDeg > 91f && angDeg < 179f)
            {
                innerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, +V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
                Debug.Log("END: Option: Angle: " + angDeg + " + assigning!");
            }
            else if (angDeg > 271f && angDeg < 359f)
            {
                innerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(-V2.x, -V2.y, 0f) + new Vector3(-V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(V2.x, V2.y, 0f) + new Vector3(V1.x, V1.y, 0f);
                Debug.Log("END: Option: Angle: " + angDeg + " + assigning!");
            }

            innerCornerDot.transform.localScale = Vector3.one * 2f;
            innerCornerDot.GetComponent<Image>().color = Color.red;
            outerCornerDot.transform.localScale = Vector3.one * 2f;
            outerCornerDot.GetComponent<Image>().color = Color.blue;

            if (!GameManager.ins.CornersFinishingDebugMode)
            {
                innerCornerDot.gameObject.SetActive(false);
                outerCornerDot.gameObject.SetActive(false);
            }

            float topDist = outerCornerDot.transform.localPosition.x;
            float botDist = innerCornerDot.transform.localPosition.x;


            if (Mathf.Abs(angDeg - 90f) < 1f)
            {
                _topMidToEnd.localScale = new Vector3(Lenght / 2 - topDist, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(Lenght / 2 - botDist, 1f, 1f) * _scaleFactor;
            }
            else if (Mathf.Abs(angDeg - 270f) < 1f)
            {
                _topMidToEnd.localScale = new Vector3(Lenght / 2 + topDist, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(Lenght / 2 + botDist, 1f, 1f) * _scaleFactor;
            }
            else if (angDeg > 91f && angDeg < 179f)
            {
                Debug.Log("Option: Angle: " + angDeg + " + drawing!");
                _topMidToEnd.localScale = new Vector3(topDist - Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(botDist - Lenght / 2, 1f, 1f) * _scaleFactor;
            }
            else if (angDeg > 271f && angDeg < 359f)
            {
                Debug.Log("Option: Angle: " + angDeg + " + drawing!");
                _topMidToEnd.localScale = new Vector3(topDist - Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(botDist - Lenght / 2, 1f, 1f) * _scaleFactor;
            }
            else
            {
                _topMidToEnd.localScale = new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor;
            }
        }
        else
        {
            _topMidToEnd.localScale = new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor;
            _bottomMidToEnd.localScale = new Vector3(Lenght / 2, 1f, 1f) * _scaleFactor;
        }
        
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
            WallSection otherSection = null;
            foreach (BasePoint point in pointsAtEndPosition)
            {
                if (point.WallSection != null && point.WallSection != this.WallSection)
                    otherSection = point.WallSection;
            }
            if (otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
                return;
            }
            //Debug.Log("End Section: Angle crossing with other section: " + WallSection.AngleBetweenDeg(otherSection).ToString());
        }
    }
}
