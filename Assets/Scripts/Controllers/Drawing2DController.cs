using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Walls2D;

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

    //[SerializeField] public Transform labelsContainer;
    [SerializeField] public Transform drawingsContainer;

    [SerializeField] List<Storey2D> _storeys2D = new List<Storey2D>();
    [SerializeField] Storey2D currentStorey;

    GameObject tmpLabel;
    GameObject tmpEmptyLabel;

    [SerializeField] float visibleStoreyThickness = 10f;
    [SerializeField] float invisibleStoreyThickness = 2f;

    public WallType CurrentWallType { get; set; }

    public List<Storey2D> Storeys2D { get { return _storeys2D; } }

    public static Drawing2DController ins { get; private set; }

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
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-_whiteboardBackground.rect.width / 2, -_whiteboardBackground.rect.height / 2, 0);        
        _storeys2D.Add(currentStorey);
    }


    public void InitializeFirstStorey(Storey storey)
    {
        currentStorey.StoreyReference = storey;
    }
    public void SwitchToStorey(Storey storey)
    {
        foreach(Storey2D st in _storeys2D)
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
        foreach(Storey2D storey in _storeys2D)
        {
            if (storey == currentStorey)
                storey.SetThickness(visibleStoreyThickness);
            else
                storey.SetThickness(invisibleStoreyThickness);

        }
    }

    public Vector2[] LinePoints
    {
        get { return _uILineRenderer.Points; }
    }

    public int LinePointsCount
    {
        get { return _uILineRenderer.Points.Length; }
    }

    public bool IsEmptyOrDefault()
    {
        return (LinePointsCount < 1)||(LinePointsCount==1 && LinePoints[0] == Vector2.zero);
    }

    public void ClearWallsLines()
    {
        _uILineRenderer.Points = new Vector2[0];
        _liveUILineRenderer.Points = new Vector2[0];
        _canvasController.ClearPointsLabels();
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
        tmpLabel.transform.position = targetPos - new Vector3(currentVector.x,currentVector.y,0)/2;
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

    public void ApplyWallToBuilding()
    {
        Wall wall = GameManager.ins.Building.CurrentStorey.AddNewWall();
        wall.WallSections = new Wall(_uILineRenderer.Points).WallSections;
        wall.WallType = Drawing2DController.ins.CurrentWallType;
    }

    public Vector3 SpawnPointLabel(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);

        GameObject tempScaler = new GameObject("TempScaler");
        label.transform.SetParent(tempScaler.transform);
        tempScaler.transform.localScale = new Vector3(GameManager.ins.ResolutionRatio.x, GameManager.ins.ResolutionRatio.y,0) * GameManager.ins.Zoom ;
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

    public void AddLinePointWithLabel(Vector3 position, bool localPosition = false)
    {        
        AddPointToLineRenderer(SpawnPointLabel(position, localPosition), _uILineRenderer);
    }

    private void AddPointToLineRenderer(Vector3 pointPos, UILineRenderer lineRenderer)
    {
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
        AddLinePointWithLabel(newPoint, false);
        _dynamicInputController.ResetDynamicInput();
    }

    public void StoreWall()
    {
        currentStorey.AddWallToStorey(_uILineRenderer.Points);
    }

    public void ClearCurrentLine()
    {
        _uILineRenderer.Points = new Vector2[0];
        _uILineRenderer.LineThickness += .1f;
        _uILineRenderer.LineThickness -= .1f;
        _uILineRenderer.enabled = false;
        _uILineRenderer.enabled = true;
    }
}
