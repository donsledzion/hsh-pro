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

    [SerializeField] DynamicInputController _dynamicInputController;

    [SerializeField] public Transform labelsContainer;

    GameObject tmpLabel;
    GameObject tmpEmptyLabel;

    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-_whiteboardBackground.rect.width / 2, -_whiteboardBackground.rect.height / 2, 0);
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
        Vector2 pointerPos = (targetPos - GameManager.ins.DrawingCanvasBackgroundLBCorner) / GameManager.ins.Zoom;
        Vector2 lastVector = Vector2.right;
        if (pointsCount > 1 && GameManager.ins.RelativeAngle)
            lastVector = _uILineRenderer.Points[pointsCount - 1] - _uILineRenderer.Points[pointsCount - 2];
        Vector2 currentVector = pointerPos - _uILineRenderer.Points[pointsCount - 1];

        float angle = Vector2.Angle(currentVector, lastVector);
        float labelAngle = Vector2.SignedAngle(currentVector, Vector2.right);

        _liveUILineRenderer.Points = new Vector2[] { lastPoint, pointerPos };

        if (tmpLabel == null) tmpLabel = Instantiate(pointLabelPrefab, new Vector3(pointerPos.x, pointerPos.y, 0), pointLabelPrefab.transform.rotation);
        if (tmpEmptyLabel == null)
        {
            tmpEmptyLabel = Instantiate(pointLabelPrefab, new Vector3(pointerPos.x, pointerPos.y, 0), pointLabelPrefab.transform.rotation);
            tmpEmptyLabel.transform.SetParent(labelsContainer);
            tmpEmptyLabel.GetComponent<PointLabel>().SetLabelText("");
            tmpEmptyLabel.transform.position = targetPos;
        }
        tmpLabel.transform.SetParent(labelsContainer);
        tmpLabel.GetComponent<PointLabel>().SetLabelText("" + Mathf.Round((pointerPos - lastPoint).magnitude) + " [cm] | " + Mathf.Round(angle * 10) / 10f);
        tmpLabel.transform.position = targetPos - new Vector3(currentVector.x,currentVector.y,0)/2;
        tmpLabel.transform.localEulerAngles = new Vector3(0, 0, -labelAngle);

        _liveUILineRenderer.LineThickness += 0.1f;
        _liveUILineRenderer.LineThickness -= 0.1f;
    }

    public void SpawnPointLabel(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);

        GameObject tempScaler = new GameObject("TempScaler");
        label.transform.SetParent(tempScaler.transform);
        tempScaler.transform.localScale = Vector3.one * GameManager.ins.Zoom;
        label.transform.SetParent(transform.root);
        label.transform.position = position;
        label.transform.SetParent(labelsContainer);

        Destroy(tempScaler);

        Vector3 ovcPos = position;
        if (localPosition == true)
        {
            ovcPos = (position - GameManager.ins.DrawingCanvasBackgroundLBCorner) / GameManager.ins.Zoom;
        }

        _uILineRenderer.AddPoint(ovcPos);
        _uILineRenderer.LineThickness += 0.1f;
        _uILineRenderer.LineThickness -= 0.1f;
        label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10 * ovcPos.x)) / 10f + " , " + ((int)(10 * ovcPos.y)) / 10f + " ]");
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
        SpawnPointLabel(newPoint, false);
        _dynamicInputController.ResetDynamicInput();
    }
}
