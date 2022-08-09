using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Walls2D;
using System.Collections;

public class Selector2D : MonoBehaviour
{
    [SerializeField] Color _hoverColor = Color.red;
    [SerializeField] Color _selectColor = Color.blue;
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

    private void Start()
    {
        _uILineRenderer = GetComponent<UILineRenderer>();
    }

    private void OnEnable()
    {
        //StoreyCreator.ins.SwitchOffAll();
    }

    private void Update()
    {
        //Check if the left Mouse button is clicked
        //if (Input.GetKey(KeyCode.Mouse0)&&!_delayCooldown)
        {
            Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
            //Debug.Log("Mouse Position [ " +mouseOverCanvas.x+ ", " + mouseOverCanvas.y +  " ]");
            HoverSection(ClosestSection(mouseOverCanvas));
            StartCoroutine("DelayCor");
        }
    }

    public WallSection ClosestSection(Vector2 mouseOverCanvas)
    {
        List<LineSection> segments = new List<LineSection>();
        List<Wall> walls = GameManager.ins.Building.CurrentStorey.Walls;
        WallSection closestSection = null;
        float closestDistance = float.MaxValue;
        foreach (Wall wall in walls)
        {
            foreach (WallSection section in wall.WallSections)
            {
                LineSection lSection = new LineSection(section);
                segments.Add(lSection);
                float distance = MathHelpers.PointToLineDistance(lSection.Start, lSection.End, mouseOverCanvas);                
                if(distance < closestDistance)
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
        if (section == null) return;
        Vector2[] points = { section.StartPoint.Position, section.EndPoint.Position };
        _uILineRenderer.Points = points;
        _uILineRenderer.color = _hoverColor;
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
        _uILineRenderer.LineThickness = 15f;
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
