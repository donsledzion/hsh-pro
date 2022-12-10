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
    [SerializeField] private GameObject _cornerDebuggerPrefab;

    [SerializeField] private GameObject _startDebugger;
    [SerializeField] private GameObject _endDebugger;

    Storey2D _storey2DReference;
    bool _startHandled = false;
    bool _endHandled = false;

    public bool hasBeenDrawn = false;

    [SerializeField] GameObject _cornerMarkerDotPrefab;

    public void AssignToStorey(Storey2D storey)
    {
        _storey2DReference = storey;
    }

    public void SetStartHandled()
    {
        _startHandled = true;
    }
    public void SetEndHandled()
    {
        _endHandled = true;
    }

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

        List<BasePoint> pointsAtPosition = CurrentStorey.BasePointsAtPosition(StartPoint, typeof(SectionStraight));
        if (pointsAtPosition.Count >= 2)
        {
            WallSection otherSection = GetTheOtherSection(pointsAtPosition);
            if (otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
                return;
            }
            SetStartHandled();
            // ============================================================================================
            float angDeg = WallSection.AngleBetweenDeg(otherSection);
            float angCompRad = (180f - angDeg) * Mathf.PI / 180;

            Vector2[] Vectors = SetupVectorsForOption2(angDeg, angCompRad, otherSection);
            Vector2 V1 = Vectors[0];
            Vector2 V2 = Vectors[1];

            GameObject innerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);
            GameObject outerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);
            Debug.Log("Angles: AngCompRad: " + angCompRad + " | AngDeg: " + angDeg + "),  V1: " + V1 + " | V2: " + V2);
            if (Mathf.Abs(angDeg - 90f) <= 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
                Debug.Log("Start: Option: Angle: " + angDeg + " + assigning!");
            }
            else if (Mathf.Abs(angDeg - 270f) <= 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                Debug.Log("Start: Option: Angle: " + angDeg + " + assigning!");
            }
            else if ((angDeg > 0f && angDeg < 89f))
            {
                innerCornerDot.transform.localPosition = new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
                Debug.Log("Start: Option: Angle: " + angDeg + " + assigning!");
            }
            else if ((angDeg > 91f && angDeg < 179f))
            {
                innerCornerDot.transform.localPosition = new Vector3(-V2.x, V2.y, 0f) + new Vector3(-V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(V2.x, V2.y, 0f) + new Vector3(V1.x, V1.y, 0f);
                Debug.Log("Start: Option: Angle: " + angDeg + " + assigning!");
            }
            else if ((angDeg > 181f && angDeg < 269f))
            {
                innerCornerDot.transform.localPosition = new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, -V1.y, 0f);
                Debug.Log("Start: Option: Angle: " + angDeg + " + assigning!");
            }
            else if ((angDeg > 271f && angDeg < 359f))
            {
                innerCornerDot.transform.localPosition = new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, -V1.y, 0f);
                Debug.Log("START: Option: Angle: " + angDeg + " + assigning!");
            }
            innerCornerDot.transform.localScale = Vector3.one * .25f;
            innerCornerDot.GetComponent<Image>().color = Color.red;
            outerCornerDot.transform.localScale = Vector3.one * .25f;
            outerCornerDot.GetComponent<Image>().color = Color.blue;

            if (!GameManager.ins.CornersFinishingDebugMode)
            {
                innerCornerDot.gameObject.SetActive(false);
                outerCornerDot.gameObject.SetActive(false);
            }

            float topDist = outerCornerDot.transform.localPosition.x;
            float botDist = innerCornerDot.transform.localPosition.x;
            Debug.Log("TopDist: " + topDist + " | BotDist: " + botDist);

            if (Mathf.Abs(angDeg - 90f) <= 1f)
            {
                _topMidToStart.localScale = new Vector3(-(Lenght / 2 + topDist), 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-(Lenght / 2 + botDist), 1f, 1f) * _scaleFactor;
            }
            else if (Mathf.Abs(angDeg - 270f) <= 1f)
            {
                Debug.Log("270 - drawing!");
                _topMidToStart.localScale = new Vector3(-(Lenght / 2 - topDist), 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-(Lenght / 2 - botDist), 1f, 1f) * _scaleFactor;
            }
            else if(angDeg > 1f && angDeg < 89f)
            {
                Debug.Log("START: Option: Angle: " + angDeg + " + drawing!");
                _topMidToStart.localScale = new Vector3(-((Lenght / 2 ) - topDist) , 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-((Lenght / 2) - botDist), 1f, 1f) * _scaleFactor;
            }
            else if(angDeg > 91f && angDeg < 179f)
            {
                Debug.Log("START: Option: Angle: " + angDeg + " + drawing!");
                _topMidToStart.localScale = new Vector3(-((Lenght / 2 ) - topDist) , 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-((Lenght / 2) - botDist), 1f, 1f) * _scaleFactor;
            }
            else if(angDeg > 181f && angDeg < 269f)
            {
                Debug.Log("START: Option: Angle: " + angDeg + " + drawing!");
                _topMidToStart.localScale = new Vector3(-((Lenght / 2 ) + topDist) , 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-((Lenght / 2) + botDist), 1f, 1f) * _scaleFactor;
            }
            else if ((angDeg > 271f && angDeg < 359f))
            {
                Debug.Log("START: Option: Angle: " + angDeg + " + drawing!");
                _topMidToStart.localScale = new Vector3(-((Lenght / 2) + topDist), 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-((Lenght / 2) + botDist), 1f, 1f) * _scaleFactor;
            }
            else
            {
                _topMidToStart.localScale = new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToStart.localScale = new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor;
            }

            _startDebugger = Instantiate(_cornerDebuggerPrefab, transform);
            CornersDebugger cornerDebugger = _startDebugger.GetComponent<CornersDebugger>();
            cornerDebugger.SetLeft("SL", angDeg, innerCornerDot.transform.localPosition+Vector3.right*40f+Vector3.up*5f);
            cornerDebugger.SetRight("SR", 360f - angDeg, outerCornerDot.transform.localPosition+Vector3.right*40f+Vector3.down*5f);
        }
        else
        {
            _topMidToStart.localScale = new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor;
            _bottomMidToStart.localScale = new Vector3(-Lenght / 2, 1f, 1f) * _scaleFactor;
        }

    }

    WallSection GetTheOtherSection(List<BasePoint> pointsAtPosition)
    {
        WallSection otherSection = null;
        foreach (BasePoint point in pointsAtPosition)
        {
            if (point.WallSection != null && point.WallSection != this.WallSection)
                otherSection = point.WallSection;
        }
        return otherSection;
    }

    Vector2[] SetupVectorsForOption2(float angDeg, float angCompRad, WallSection otherSection)
    {
        Vector2 V1 = new Vector2();
        Vector2 V2 = new Vector2((otherSection.Thickness / 2f) / Mathf.Sin(angCompRad), 0f);
        SectionStraight2D otherSection2D = OtherSection(otherSection);
        if ((otherSection.StartPoint.Position - WallSection.EndPoint.Position).magnitude <= 5f)
        {
            V1 = new Vector2(-(Thickness / 2f) * Mathf.Cos(angCompRad) / Mathf.Sin(angCompRad), Thickness / 2);
            if (angDeg > 1 && angDeg < 89)
                V1 = new Vector2(-(Thickness / 2f) * Mathf.Sin(angCompRad - Mathf.PI / 2) / Mathf.Cos(angCompRad - Mathf.PI / 2), Thickness / 2);
            if(otherSection2D != null)
                otherSection2D.SetStartHandled();
        }
        else
        {
            V1 = new Vector2(-(Thickness / 2f) * Mathf.Cos(angCompRad) / Mathf.Sin(angCompRad), Thickness / 2);
            if (angDeg > 1 && angDeg < 89)
                V1 = new Vector2(-(Thickness / 2f) * Mathf.Sin(angCompRad - Mathf.PI / 2) / Mathf.Cos(angCompRad - Mathf.PI / 2), Thickness / 2);
            if (otherSection2D != null)
                otherSection2D.SetEndHandled();
        }
        return new Vector2[2] { V1, V2};
    }

    SectionStraight2D OtherSection(WallSection otherSection)
    {
        SectionStraight2D other2DSection = null;
        Storey2D myStorey = GetComponentInParent<Storey2D>();
        foreach (WallSection2D section in myStorey.WallSections2D)
        {
            if(section.GetType() == typeof(SectionStraight2D))
            {
                if (section.WallSection == otherSection)
                    return section as SectionStraight2D;
            }
        }

        return other2DSection;
    }

    private void HandleEndSite()
    {
        List<BasePoint> pointsAtPosition = CurrentStorey.BasePointsAtPosition(EndPoint, typeof(SectionStraight));
        if (pointsAtPosition.Count >= 2)
        {
            WallSection otherSection = GetTheOtherSection(pointsAtPosition);
            if (otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
                return;
            }
            SetEndHandled();
            float angDeg = WallSection.AngleBetweenDeg(otherSection);
            float angCompRad = (180f - angDeg) * Mathf.PI / 180;

            Vector2[] Vectors = SetupVectorsForOption2(angDeg,angCompRad,otherSection);
            Vector2 V1 = Vectors[0];
            Vector2 V2 = Vectors[1];

            GameObject innerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);
            GameObject outerCornerDot = Instantiate(_cornerMarkerDotPrefab, transform);

            if (Mathf.Abs(angDeg - 90f) <= 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
            }
            else if (Mathf.Abs(angDeg - 270f) <= 1f)
            {
                innerCornerDot.transform.localPosition = new Vector3(otherSection.Thickness / 2f, -WallSection.Thickness / 2f);
                outerCornerDot.transform.localPosition = new Vector3(-otherSection.Thickness / 2f, WallSection.Thickness / 2f);
            }
            else if (angDeg > 1f && angDeg < 89f)
            {
                innerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
            }
            else if (angDeg > 91f && angDeg < 179f)
            {
                innerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(-V2.x, V2.y, 0f) + new Vector3(-V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(V2.x, V2.y, 0f) + new Vector3(V1.x, V1.y, 0f);
            }
            else if (angDeg > 181f && angDeg < 269f) 
            {
                innerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
            }
            else if (angDeg > 271f && angDeg < 359f)
            {
                innerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(-V2.x, V2.y, 0f) + new Vector3(V1.x, -V1.y, 0f);
                outerCornerDot.transform.localPosition = new Vector3(WallSection.Length,0f,0f) + new Vector3(V2.x, V2.y, 0f) + new Vector3(-V1.x, V1.y, 0f);
            }

            innerCornerDot.transform.localScale = Vector3.one * .75f;
            innerCornerDot.GetComponent<Image>().color = Color.green;
            outerCornerDot.transform.localScale = Vector3.one * .75f;
            outerCornerDot.GetComponent<Image>().color = Color.cyan;

            if (!GameManager.ins.CornersFinishingDebugMode)
            {
                innerCornerDot.gameObject.SetActive(false);
                outerCornerDot.gameObject.SetActive(false);
            }

            float topDist = outerCornerDot.transform.localPosition.x;
            float botDist = innerCornerDot.transform.localPosition.x;


            if (Mathf.Abs(angDeg - 90f) <= 1f)
            {
                _topMidToEnd.localScale = new Vector3(Lenght / 2 - topDist, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(Lenght / 2 - botDist, 1f, 1f) * _scaleFactor;
            }
            else if (Mathf.Abs(angDeg - 270f) <= 1f)
            {
                _topMidToEnd.localScale = new Vector3(Lenght / 2 + topDist, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(Lenght / 2 + botDist, 1f, 1f) * _scaleFactor;
            }
            else if (angDeg > 1f && angDeg < 89f)
            {
                Debug.Log("Option: Angle: " + angDeg + " + drawing!");
                _topMidToEnd.localScale = new Vector3(topDist - Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(botDist - Lenght / 2, 1f, 1f) * _scaleFactor;
            }
            else if (angDeg > 91f && angDeg < 179f)
            {
                Debug.Log("Option: Angle: " + angDeg + " + drawing!");
                _topMidToEnd.localScale = new Vector3(topDist - Lenght / 2, 1f, 1f) * _scaleFactor;
                _bottomMidToEnd.localScale = new Vector3(botDist - Lenght / 2, 1f, 1f) * _scaleFactor;
            }
            else if (angDeg > 181f && angDeg < 269f)
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
            _startDebugger = Instantiate(_cornerDebuggerPrefab, transform);
            CornersDebugger cornerDebugger = _startDebugger.GetComponent<CornersDebugger>();
            cornerDebugger.SetLeft("EL", angDeg, outerCornerDot.transform.localPosition + Vector3.left * 40f+Vector3.down*5);
            cornerDebugger.SetRight("ER", 360f - angDeg, innerCornerDot.transform.localPosition + Vector3.left * 40f + Vector3.down * 5);

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
        if (pointsAtStartPosition.Count == 1/* || pointsAtStartPosition.Count > 2*/)
            _start.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
    }

    private void HandleEnd()
    {
        List<BasePoint> pointsAtPosition = CurrentStorey.BasePointsAtPosition(EndPoint, typeof(SectionStraight));
        if (pointsAtPosition.Count == 1/* || pointsAtPosition.Count > 2*/)
        {
            _end.localScale = new Vector3(1f, Thickness * _scaleFactor, 1f);
            _end.localPosition = new Vector3(Lenght * _scaleFactor, 0f, 0f);
        }
        else if(pointsAtPosition.Count == 2)
        {
            WallSection otherSection = null;
            foreach (BasePoint point in pointsAtPosition)
            {
                if (point.WallSection != null && point.WallSection != this.WallSection)
                    otherSection = point.WallSection;
            }
            if (otherSection == null)
            {
                Debug.LogWarning("Other section is null: exiting!");
                return;
            }
        }
    }
}
