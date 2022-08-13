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
    [SerializeField] float _lineThickness = 15f;
    bool _delayCooldown = false;
    [SerializeField] UILineRenderer _hoveredUILineRenderer;
    [SerializeField] UILineRenderer _selectedUILineRenderer;

    WallSection _hoveredSection;
    WallSection _selectedSection;

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
            HoverPoint(ClosestPoint(mouseOverCanvas));
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            SelectSection(_hoveredSection);            
        }
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            WallSectionDeleter.DeleteSection(_selectedSection);
            Unselect();
            Unhover();
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

    void Unselect()
    {
        ClearLine(_selectedUILineRenderer);
        _selectedSection = null;
    }

    void Unhover()
    {
        ClearLine(_hoveredUILineRenderer);
        _hoveredSection = null;
    }

    void SelectSection(WallSection section)
    {
        HighlightSection(section,_selectedUILineRenderer,_selectColor);
        _selectedSection = section;
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
