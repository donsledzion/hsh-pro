using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI.Extensions;
using Walls2D;
using static Selector2D;

public class Drawing2DController : MonoBehaviour
{
    [SerializeField] RectTransform _whiteboardBackground;
    [SerializeField] UILineRenderer _uILineRenderer;
    [SerializeField] UILineRenderer _liveUILineRenderer;
    [SerializeField] CanvasController _canvasController;

    [SerializeField] GameObject pointLabelPrefab;
    [SerializeField] GameObject storey2DPrefab;
    [SerializeField] GameObject wallOnCanvasPrefab;


    [SerializeField] DynamicInputController _dynamicInputController;


    float _firstToLastTollerance = 0.2f;

    //[SerializeField] public Transform labelsContainer;
    [SerializeField] public Transform drawingsContainer;

    [SerializeField] List<Storey2D> _storeys2D = new List<Storey2D>();
    [SerializeField] Storey2D currentStorey;

    GameObject tmpLabel;
    GameObject tmpEmptyLabel;

    [SerializeField] float visibleStoreyThickness = 10f;
    [SerializeField] float invisibleStoreyThickness = 2f;

    public Building Building => GameManager.ins.Building;
    public Storey BuildingCurrentStorey => GameManager.ins.Building.CurrentStorey;

    public List<Storey> BuildingStoreys => GameManager.ins.Building.Storeys;    

    public Storey2D CurrentStorey { get { return currentStorey; } }
    public WallType CurrentWallType { get; set; }

    public Vector2[] CurrentStoreyPoints => currentStorey.Points;

    public Point2DInfo[] CurrentStoreyInfoPoints => currentStorey.Points2DInfo;

    public List<Storey2D> Storeys2D { get { return _storeys2D; } }

    public static Drawing2DController ins { get; private set; }
    public Vector2[] LinePoints
    {
        get { return _uILineRenderer.Points; }
    }

    public int LinePointsCount
    {
        get { return _uILineRenderer.Points.Length; }
    }

    public float FirstToLastTollerance => _firstToLastTollerance;
    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    private void OnEnable()
    {
        AdjustCanvasPosition();
    }

    public void AdjustCanvasPosition()
    {
        gameObject.GetComponent<RectTransform>().localPosition =
            new Vector3(-_whiteboardBackground.rect.width / 2, -_whiteboardBackground.rect.height / 2, 0);
    }

    [ContextMenu("Erase Storeys")]
    public void EraseStoreys()
    {
        if (_storeys2D.Count < 1) return;
        foreach (Storey2D storey2d in _storeys2D)
        {
            Destroy(storey2d.gameObject);
        }            
        _storeys2D.Clear();
        currentStorey = null;
    }

    public void LoadBuilding()
    {
        EraseStoreys();
        if (BuildingStoreys.Count <= 1) return;
        for (int i = 0; i < BuildingStoreys.Count; i++)
        {
            GameObject newStorey = Instantiate(storey2DPrefab, drawingsContainer);
            newStorey.name = BuildingStoreys[i].Name;
            Storey2D storey = newStorey.GetComponent<Storey2D>();
            storey.StoreyReference = BuildingStoreys[i];
            Storeys2D.Add(storey);
            Debug.Log("Initializing storey no. " + i + " named: " + storey.StoreyReference.Name);
            DrawStorey(BuildingStoreys[i]);
        }
        currentStorey = Storeys2D[0];
    }

    public void InitializeFirstStorey(Storey storey)
    {
        if(Storeys2D.Count <= 1)
        {
            GameObject firstStorey = Instantiate(storey2DPrefab, drawingsContainer);
            firstStorey.name = storey.Name;
            Storeys2D.Add(firstStorey.GetComponent<Storey2D>());            
        }
        if (currentStorey == null)
            currentStorey = Storeys2D[0];
        currentStorey.StoreyReference = storey;
    }
    public void SwitchToStorey(Storey storey)
    {
        foreach (Storey2D st in _storeys2D)
        {
            if (st.StoreyReference == storey)
            {
                currentStorey = st;
                SetStoreysVisibility();
                return;
            }
        }        
        GameObject newStorey = Instantiate(storey2DPrefab, drawingsContainer);
        newStorey.name = storey.Name;
        newStorey.transform.SetParent(drawingsContainer);
        currentStorey = newStorey.GetComponent<Storey2D>();
        currentStorey.StoreyReference = storey;
        _storeys2D.Add(currentStorey);
        SetStoreysVisibility();
    }

    void SetStoreysVisibility()
    {
        foreach (Storey2D storey in _storeys2D)
        {
            if (storey == currentStorey)
                storey.SetVisibiilty(true);
            else
                storey.SetVisibiilty(false);
        }
    }

    [ContextMenu("RegenerateBuildingDrawing")]
    public void RegenerateBuildingDrawing()
    {
        LoadBuilding();
        SetStoreysVisibility();
    }


    public Wall ApplyWallToBuilding()
    {
        Wall wall = BuildingCurrentStorey.AddNewWall();
        wall.WallSections = new Wall(_uILineRenderer.Points).WallSections;
        wall.AssignSections();
        wall.WallType = CurrentWallType;
        return wall;
    }
    public void StoreWall(Wall wall)
    {
        currentStorey.AddWallToStorey(wall);
    }

    public void StoreFloor(FloorSection2D floor)
    {
        currentStorey.AddFloorToStorey(floor);
    }

    public void DrawStorey(Storey storey)
    {
        Storey2D storey2D = null;
        foreach (Storey2D inspectedStorey in Storeys2D)
        {
            if(inspectedStorey.StoreyReference == storey)
            {
                storey2D=inspectedStorey;
            }
        }
        if (storey2D == null) return;
        foreach(Wall wall in storey.Walls)
        {
            storey2D.AddWallToStorey(wall);
        }
        foreach (FloorSection2D floor in storey.Floors)
        {
            storey2D.AddFloorToStorey(floor);
        }

    }
    
    [ContextMenu("RedrawCurrentStorey")]
    public void RedrawCurrentStorey()
    {
        currentStorey.ClearStorey2D();
        DrawStorey(BuildingCurrentStorey);
        DrawLabels();
    }


    [ContextMenu("Draw Labels")]
    void DrawLabels()
    {
        foreach(Wall wall in currentStorey.StoreyReference.Walls)
        {
            foreach (Vector2 point in wall.Points2D)
                SpawnPointLabelOnRedraw(point,false);
        }
    }

    void ClearCurrentStorey()
    {
        ClearStorey(currentStorey);
    }

    void ClearStorey(Storey2D storey)
    {
        storey.ClearStorey2D();
    }
    public void DrawLive(Vector3 targetPos)
    {
        if (IsEmptyOrDefault()) return;
        int pointsCount = _uILineRenderer.Points.Length;
        if (pointsCount < 1 || (_uILineRenderer.Points[0].x == 0f && _liveUILineRenderer.Points[0].y == 0f)) return;

        Vector2 lastPoint = _uILineRenderer.Points[pointsCount - 1];
        Vector2 pointerPos = CanvasController.ScreenPointToCanvasCoords(targetPos);
        Vector2 lastVector = Vector2.right;

        if (pointsCount > 1 && GameManager.ins.RelativeAngle)
            lastVector = _uILineRenderer.Points[pointsCount - 1] - _uILineRenderer.Points[pointsCount - 2];
        Vector2 currentVector = pointerPos - _uILineRenderer.Points[pointsCount - 1];

        float angle = Vector2.Angle(currentVector, lastVector);
        float labelAngle = Vector2.SignedAngle(currentVector, Vector2.right);

        _liveUILineRenderer.Points = new Vector2[] { lastPoint, pointerPos };

        if (tmpLabel == null)
        {
            tmpLabel = Instantiate(pointLabelPrefab, new Vector3(pointerPos.x, pointerPos.y, 0), pointLabelPrefab.transform.rotation);
            tmpLabel.transform.SetParent(currentStorey.LabelsContainer);
        }
        if (tmpEmptyLabel == null)
        {
            tmpEmptyLabel = Instantiate(pointLabelPrefab, new Vector3(pointerPos.x, pointerPos.y, 0), pointLabelPrefab.transform.rotation);

            tmpEmptyLabel.GetComponent<PointLabel>().SetLabelText("");
            tmpEmptyLabel.transform.SetParent(currentStorey.LabelsContainer);

        }
        tmpEmptyLabel.transform.position = targetPos;
        tmpLabel.GetComponent<PointLabel>().SetLabelText("" + Mathf.Round((pointerPos - lastPoint).magnitude) + " [cm] | " + Mathf.Round(angle * 10) / 10f);
        tmpLabel.transform.position = targetPos - new Vector3(currentVector.x, currentVector.y, 0) / 2;
        tmpLabel.transform.localEulerAngles = new Vector3(0, 0, -labelAngle);

        _liveUILineRenderer.LineThickness += 0.1f;
        _liveUILineRenderer.LineThickness -= 0.1f;
    }

    public void ClearLiveLine()
    {
        _liveUILineRenderer.Points = new Vector2[0];
        _liveUILineRenderer.enabled = false;
        _liveUILineRenderer.enabled = true;
        Destroy(tmpLabel);
        Destroy(tmpEmptyLabel);
    }
    public Vector3 SpawnPointLabel(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);

        GameObject tempScaler = new GameObject("TempScaler");
        label.transform.SetParent(tempScaler.transform);
        tempScaler.transform.localScale = new Vector3(GameManager.ins.ResolutionRatio.x, GameManager.ins.ResolutionRatio.y, 0) * GameManager.ins.Zoom;
        label.transform.SetParent(transform.root);
        label.transform.position = position;
        label.transform.SetParent(currentStorey.LabelsContainer);

        Destroy(tempScaler);

        Vector3 ovcPos = position;
        if (localPosition == true)
            ovcPos = CanvasController.ScreenPointToCanvasCoords(position);
        else
            label.transform.position = position + GameManager.ins.DrawingCanvasBackgroundLBCorner;
        label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10 * ovcPos.x)) / 10f + " , " + ((int)(10 * ovcPos.y)) / 10f + " ]");
        return ovcPos;
    }

    public Vector3 SpawnPointLabelOnRedraw(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);

        GameObject tempScaler = new GameObject("TempScaler");
        label.transform.SetParent(tempScaler.transform);
        tempScaler.transform.localScale = new Vector3(GameManager.ins.ResolutionRatio.x, GameManager.ins.ResolutionRatio.y, 0) * GameManager.ins.Zoom;
        label.transform.SetParent(currentStorey.LabelsContainer);
        label.transform.localPosition = position;

        Destroy(tempScaler);

        Vector3 ovcPos = position;

        label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10 * ovcPos.x)) / 10f + " , " + ((int)(10 * ovcPos.y)) / 10f + " ]");
        return ovcPos;

    }

    public void AddLinePoint(Vector3 position, bool localPosition, bool withLabel)
    {
        if (withLabel)
            AddLinePointWithLabel(position, localPosition);
        else
            AddPointToLineRenderer(position,_uILineRenderer);
    }

    public void AddLinePointWithLabel(Vector3 position, bool localPosition = false)
    {
        AddPointToLineRenderer(SpawnPointLabel(position, localPosition), _uILineRenderer);
    }

    private void AddPointToLineRenderer(Vector3 pointPos, UILineRenderer lineRenderer)
    {
        /*
         *  TODO: Hear need to implement breaking line into separate wall sections.
         */
        lineRenderer.AddPoint(pointPos);
        lineRenderer.LineThickness += 0.1f;
        lineRenderer.LineThickness -= 0.1f;
    }

    public Vector2 CurrentSectionVector(Vector3 targetPos)
    {
        int pointsCount = _uILineRenderer.Points.Length;
        Vector2 pointerPos = (targetPos - GameManager.ins.DrawingCanvasBackgroundLBCorner) / GameManager.ins.Zoom;
        return pointerPos - _uILineRenderer.Points[pointsCount - 1];
    }
    public void ApplyDynamicInput(Vector3 pointerPosition)
    {
        int pointsCount = LinePointsCount;
        Vector2 lastPoint = LinePoints[pointsCount - 1];
        Vector2 currentVector = CurrentSectionVector(pointerPosition);
        Vector2 newPoint = lastPoint + currentVector.normalized * int.Parse(_dynamicInputController.DynamicInputString);
        AddLinePoint(newPoint, false, false);
        //AddLinePointWithLabel(newPoint, false);
        _dynamicInputController.ResetDynamicInput();
    }

    public void ClearCurrentLine()
    {
        _uILineRenderer.Points = new Vector2[0];
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
        _uILineRenderer.enabled = false;
        _uILineRenderer.enabled = true;
    }

    public void ClearWallsLines()
    {
        _uILineRenderer.Points = new Vector2[0];
        _liveUILineRenderer.Points = new Vector2[0];
        _canvasController.ClearPointsLabels();
    }
    public bool IsEmptyOrDefault()
    {
        return (LinePointsCount < 1) || (LinePointsCount == 1 && LinePoints[0] == Vector2.zero);
    }
}
