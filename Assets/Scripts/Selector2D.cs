using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using System.Collections.Generic;
using Walls2D;
using System.Collections;

public class Selector2D : MonoBehaviour
{
    [SerializeField] Color _hoverColor = Color.red;
    [SerializeField] Color _selectColor = Color.blue;
    [SerializeField] Color _defaultColor = Color.black;

    [SerializeField] float _lineSnapDistance = 10f;
    [SerializeField] float _pointSnapDistance = 10f;

    [SerializeField] GameObject dotPrefab;
    GameObject _dotInstance;
    [SerializeField] SelectionType _selectionType = SelectionType.Line;

    bool _delayCooldown = false;
    UILineRenderer _uILineRenderer;
    internal struct LineSection
    {
        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public LineSection(WallSection section)
        {
            Start = section.StartPoint.Position;
            End = section.EndPoint.Position;
        }
    }

    enum SelectionType
    {
        Point,
        Line,
        Wall,
    }

    private void Start()
    {
        _uILineRenderer = GetComponent<UILineRenderer>();
    }

    private void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        if(_selectionType == SelectionType.Line)
            HoverSection(ClosestSection(mouseOverCanvas));
        else if(_selectionType == SelectionType.Point)
            HoverPoint(ClosestPoint(mouseOverCanvas));
        StartCoroutine("DelayCor");
    }

    public WallSection ClosestSection(Vector2 mouseOverCanvas)
    {
        List<LineSection> segments = new List<LineSection>();
        List<Wall> walls = GameManager.ins.Building.CurrentStorey.Walls;
        WallSection closestSection = null;
        float closestDistance = _lineSnapDistance;
        foreach (Wall wall in walls)
        {
            foreach (WallSection section in wall.WallSections)
            {
                LineSection lSection = new LineSection(section);
                segments.Add(lSection);
                float distance = MathHelpers.PointToLineDistance(lSection.Start, lSection.End, mouseOverCanvas);
                bool pointWithinSection = MathHelpers.DoesPointCastsOnLine(lSection.Start, lSection.End, mouseOverCanvas);
                if((distance < closestDistance) && pointWithinSection)
                {
                    closestDistance = distance;
                    closestSection = section;
                    //Debug.Log("Section distance " + MathHelpers.PointToLineDistance(lSection.Start, lSection.End, mouseOverCanvas));
                }                
            }
        }
        return closestSection;
    }

    void HoverSection(WallSection section)
    {
        if (section == null)
        {
            if(_uILineRenderer.color != _defaultColor)
                ClearLine();
            return;
        }
        _uILineRenderer.enabled = true;
        Vector2[] points = { section.StartPoint.Position, section.EndPoint.Position };
        _uILineRenderer.Points = points;
        _uILineRenderer.color = _hoverColor;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
        _uILineRenderer.LineThickness = 15f;
    }

    void HoverPoint(Vector2 point)
    {
        if(point != new Vector2())
        {
            if (!_dotInstance)
                _dotInstance = Instantiate(dotPrefab);
            _dotInstance.transform.SetParent(transform);
            _dotInstance.transform.localPosition = point;
            _dotInstance.transform.localScale = 5*Vector3.one;
            _dotInstance.GetComponent<Image>().color = _hoverColor;
        }
        else
        {
            Destroy(_dotInstance);
        }
    }

    Vector2 ClosestPoint(Vector2 mouseInput)
    {
        Vector2[] points = Drawing2DController.ins.CurrentStoreyPoints;
        Debug.Log("Points count: " + points.Length);
        Vector2 closestPoint = new Vector2();
        float maxDist = _pointSnapDistance;
        foreach(Vector2 point in points)
        {
            if((point - mouseInput).magnitude < maxDist)
            {
                maxDist = (point - mouseInput).magnitude;
                closestPoint = point;
            }
        }
        return closestPoint;
    }

    void ClearLine()
    {
        _uILineRenderer.enabled = false;
        Debug.Log("Clearing line...");
        Vector2[] points = { };
        _uILineRenderer.color = _defaultColor;
        _uILineRenderer.Points = points;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
    }

    public void SelectWall()
    {
        gameObject.SetActive(true);
    }

    IEnumerator DelayCor()
    {
        _delayCooldown = true;
        yield return new WaitForSeconds(.1f);
        _delayCooldown = false;
    }
}
