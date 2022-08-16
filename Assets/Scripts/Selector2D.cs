using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using System.Collections.Generic;
using Walls2D;
using System.Collections;
using System;

public class Selector2D : MonoBehaviour
{
    [SerializeField] Color _hoverColor = Color.red;
    [SerializeField] Color _selectColor = Color.blue;
    [SerializeField] Color _defaultColor = Color.black;

    [SerializeField] float _lineSnapDistance = 10f;
    [SerializeField] float _pointSnapDistance = 10f;

    [SerializeField] GameObject dotPrefab;
    GameObject _hoveringDotInstance;
    GameObject _selectingDotInstance;
    [SerializeField] SelectionType _selectionType = SelectionType.Line;
    [SerializeField] float _lineThickness = 15f;
    bool _delayCooldown = false;
    [SerializeField] UILineRenderer _hoveredUILineRenderer;
    [SerializeField] UILineRenderer _selectedUILineRenderer;

    protected WallSection _hoveredSection;
    protected WallSection _selectedSection;

    Vector2 _hoveredPoint;
    Vector2 _selectedPoint;



    bool _pointDrag = false;

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

    private void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        if (_selectionType == SelectionType.Line)
        {
            _hoveredSection = ClosestSection(mouseOverCanvas);
            HoverSection(_hoveredSection);
        }
        else if (_selectionType == SelectionType.Point)
        {
            if(_hoveredSection!= null)
                _hoveredSection = null;
            HoverPoint(ClosestPoint(mouseOverCanvas).Point);
        }
        
        if(Input.GetMouseButtonDown(0))
        {   
            
            if (_selectionType == SelectionType.Line)
                SelectSection(_hoveredSection);            
            else if(_selectionType == SelectionType.Point)
            {
                _pointDrag = true;
                SelectPoint(_hoveredPoint);
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            WallSectionDeleter.DeleteSection(_selectedSection);
            Unselect();
            Unhover();
        }

        if(Input.GetMouseButtonUp(0))
        {
            _pointDrag = false;
        }

        if(_pointDrag && _selectingDotInstance!=null)
        {
            Debug.Log("Dragging point");
            DragPoint.DragPointTo(_selectedPoint, CanvasController.ScreenPointToCanvasCoords(Input.mousePosition));
        }
        else
        {
            Debug.Log("NOT Dragging point");
        }

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
                }                
            }
        }
        return closestSection;
    }

    void HoverSection(WallSection section)
    {
        if (section != null && section != _selectedSection)
        {
            HighlightSection(section, _hoveredUILineRenderer,_hoverColor);
            _hoveredSection = section;
        }
    }

    void HighlightSection(WallSection section, UILineRenderer uiLineRenderer, Color highlightColor)
    {
        if (section == null)
        {
            if (uiLineRenderer.color != _defaultColor)
                ClearLine(uiLineRenderer);
            return;
        }
        uiLineRenderer.enabled = true;
        Vector2[] points = { section.StartPoint.Position, section.EndPoint.Position };
        uiLineRenderer.Points = points;
        uiLineRenderer.color = highlightColor;
        uiLineRenderer.LineThickness = _lineThickness;
    }

    protected void Unselect()
    {
        ClearLine(_selectedUILineRenderer);
        _selectedUILineRenderer.enabled = false;
        _selectedSection = null;
    }

    protected void Unhover()
    {
        ClearLine(_hoveredUILineRenderer);
        _hoveredUILineRenderer.enabled = false;
        _hoveredSection = null;
    }

    protected virtual void SelectSection(WallSection section)
    {
        HighlightSection(section,_selectedUILineRenderer,_selectColor);
        _selectedSection = section;

    }

    void SelectPoint(Vector2 point)
    {
        if(_hoveringDotInstance!=null && (point == _hoveredPoint)/* && (point != _selectedPoint)*/)
        {
            if(_selectingDotInstance == null)
                _selectingDotInstance = Instantiate(dotPrefab);
            _selectingDotInstance.transform.SetParent(transform);
            _selectingDotInstance.transform.localPosition = point;
            _selectingDotInstance.transform.localScale = 5 * Vector3.one;
            _selectingDotInstance.GetComponent<Image>().color = _selectColor;
            _selectedPoint = point;
        }        
    }
    

    void HoverPoint(Vector2 point)
    {
        if (point != null && point != _hoveredPoint)
        {
            if (point != new Vector2(0,0))
            {
                if (_hoveringDotInstance== null)
                    _hoveringDotInstance = Instantiate(dotPrefab);
                _hoveringDotInstance.transform.SetParent(transform);
                _hoveringDotInstance.transform.localPosition = point;
                _hoveringDotInstance.transform.localScale = 5 * Vector3.one;
                _hoveringDotInstance.GetComponent<Image>().color = _hoverColor;
                _hoveredPoint = point;
            }
            else
            {
                _hoveredPoint = new Vector2(0,0);
                Destroy(_hoveringDotInstance);
            }
        }            
    }

    Point2DInfo ClosestPoint(Vector2 mouseInput)
    {
        Point2DInfo[] points = Drawing2DController.ins.CurrentStoreyInfoPoints;
        Debug.Log("Points count: " + points.Length);
        Point2DInfo closestPoint = new Point2DInfo();
        float maxDist = _pointSnapDistance;
        foreach(Point2DInfo point in points)
        {
            if((point.Point - mouseInput).magnitude < maxDist)
            {
                maxDist = (point.Point - mouseInput).magnitude;
                closestPoint = point;
            }
        }
        return closestPoint;
    }

    void ClearLine(UILineRenderer uILineRenderer)
    {
        _hoveredUILineRenderer.enabled = false;
        Debug.Log("Clearing line...");
        Vector2[] points = { };
        uILineRenderer.color = _defaultColor;
        uILineRenderer.Points = points;
        uILineRenderer.LineThickness += .1f;
        uILineRenderer.LineThickness -= .1f;
    }

    protected void ClearSelectingLine()
    {
        ClearLine(_selectedUILineRenderer);
    }

    protected void ClearHoveringLine()
    {
        ClearLine(_hoveredUILineRenderer);
    }

    protected void ClearLines()
    {
        ClearSelectingLine();
        ClearHoveringLine();
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

    public struct Section2DInfo
    {
        WallSection _section;
        Wall _wall;

        public Section2DInfo(WallSection section, Wall wall)
        {
            _section = section;
            _wall = wall;
        }

        public WallSection Section { get { return _section; } }
        public Wall Wall { get { return _wall; } }
    }

    public struct Point2DInfo
    {
        Vector2 _point;
        Wall _wall;

        public Point2DInfo(Vector2 point, Wall wall)
        {
            _point = point;
            _wall = wall;
        }


        public Vector2 Point { get { return _point; } }
        public Wall Wall{ get { return _wall; } }
    }
}
