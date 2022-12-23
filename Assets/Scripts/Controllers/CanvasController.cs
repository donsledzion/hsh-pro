using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public static CanvasController ins { get; private set; }

    [SerializeField] public GameObject drawingCanvas;
    [SerializeField] TextMeshProUGUI pointerPositionTMPro;
    [SerializeField] TextMeshProUGUI CanvasCoordsTMPro;
    [SerializeField] public ZoomController pointerController;
    [SerializeField] public ClickerController clickerController;
    //======================================================================
    Canvas mainCanvas;
    public Vector3 drawingCanvasBackgroundLBCorner;


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

    private void Start()
    {
        mainCanvas = gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        pointerPositionTMPro.text = ("Pointer [ " + Input.mousePosition.x + " , " + Input.mousePosition.y + " ]");
        UpdateCanvasCoords(ScreenPointToCanvasCoords(Input.mousePosition));
    }

    public void RedrawCanvas(Vector3 mousePosition, float currentScale, float previousScale)
    {
        Vector3 mouse2centerOffset = drawingCanvas.transform.position - mousePosition;
        //Debug.Log("m2cOffset: " + mouse2centerOffset);
        drawingCanvas.transform.position = mousePosition + (mouse2centerOffset / previousScale) * (currentScale);
        drawingCanvas.transform.localScale = Vector3.one * currentScale;
    }

    private void UpdateCanvasCoords(Vector2 coords)
    {
        CanvasCoordsTMPro.text = "Canvas coords [" + coords.x + "," + coords.y + "]";
    }

    public static Vector2 ScreenPointToCanvasCoords(Vector2 inputCoords, float correctX = 1f, float correctY = 1f)
    {
        Vector2 outCoords = inputCoords - new Vector2(
            GameManager.ins.DrawingCanvasBackgroundLBCorner.x * correctX,
            GameManager.ins.DrawingCanvasBackgroundLBCorner.y * correctY);

        return new Vector2(
            outCoords.x/*/GameManager.ins.ResolutionRatio.x*/,
            outCoords.y/*/GameManager.ins.ResolutionRatio.y*/)
            / GameManager.ins.Zoom;
    }

    public static Vector2 CanvasCoordsToScreenPoint(Vector2 canvasCoords)
    {
        Vector2 outCoords = new Vector2(
            canvasCoords.x * GameManager.ins.ResolutionRatio.x,
            canvasCoords.y * GameManager.ins.ResolutionRatio.y)
            * GameManager.ins.Zoom;

        return new Vector2(
            outCoords.x + GameManager.ins.DrawingCanvasBackgroundLBCorner.x,
            outCoords.y + GameManager.ins.DrawingCanvasBackgroundLBCorner.y);
    }

    [ContextMenu("Reset Canvas")]
    public void ResetCanvas()
    {
        ResetCanvasPosition();
        ResetCanvasScale();
    }

    [ContextMenu("Reset Canvas Position")]
    public void ResetCanvasPosition()
    {
        RectTransform mainRect = mainCanvas.GetComponent<RectTransform>();
        drawingCanvas.transform.position = new Vector3(
            mainRect.rect.width / 2,
            mainRect.rect.height / 2, 0);
    }

    [ContextMenu("Reset Canvas Scale")]
    public void ResetCanvasScale()
    {
        GameManager.ins.Zoom = 1f;
        drawingCanvas.transform.localScale = Vector3.one;
    }

    public void ClearPointsLabels()
    {
        /*foreach (PointLabel label in clickerController.labelsContainer.GetComponentsInChildren<PointLabel>())
            Destroy(label.gameObject);*/
        /*
         * Needs recreation to display (and destroy) labels for separate storeys
         */
    }
}
