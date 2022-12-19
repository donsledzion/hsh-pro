using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using System.Collections.Generic;
using Walls2D;
using System.Collections;
using System;

public class Selector2D : Point2DSelector
{
    [SerializeField] protected Color _hoverColor = Color.red;
    [SerializeField] protected Color _selectColor = Color.blue;
    [SerializeField] protected Color _defaultColor = Color.black;

    [SerializeField] protected float _lineSnapDistance = 10f;

    [SerializeField] SelectionType _selectionType = SelectionType.Line;
    [SerializeField] float _lineThickness = 15f;
    bool _delayCooldown = false;
    [SerializeField] UILineRenderer _hoveredUILineRenderer;
    [SerializeField] UILineRenderer _selectedUILineRenderer;

    protected WallSection _hoveredSection;
    protected WallSection _selectedSection;
    protected JambSectionValidationToggler _validationToggler;



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

    protected virtual void Update()
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
            HoverPoint(ClosestPoint(mouseOverCanvas).Point, _hoverColor);
        }
        
        if(Input.GetMouseButtonDown(0))
        {               
            if (_selectionType == SelectionType.Line)
                SelectSection(_hoveredSection);            
            else if(_selectionType == SelectionType.Point)
            {
                _pointDrag = true;
                SelectPoint(_hoveredPoint, _selectColor);
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

    public virtual WallSection ClosestSection(Vector2 mouseOverCanvas)
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
        /*if(closestSection != null)
            Debug.Log("Closest section wall: " + closestSection.Wall);*/
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

    
    void ClearLine(UILineRenderer uILineRenderer)
    {
        _hoveredUILineRenderer.enabled = false;
        /*Debug.Log("Clearing line...");*/
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

    public Vector2 CastedPoint(WallSection section, Vector2 mouseInput)
    {
        return MathHelpers.PointCastOnLine(section.StartPoint.Position, section.EndPoint.Position, mouseInput);
    }

    protected bool ValidatePosition(Vector2 startEdge, Vector2 endEdge)
    {
        bool leftIsGood = _hoveredSection.PointLaysWithinSection(startEdge);
        bool rightIsGood = _hoveredSection.PointLaysWithinSection(endEdge);

        bool validationPassed = leftIsGood && rightIsGood;

        if (validationPassed && !_validationToggler.IsGood) _validationToggler.BeGood();
        else if (!validationPassed && _validationToggler.IsGood) _validationToggler.BeBad();

        return validationPassed;
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
