using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [SerializeField] public GameObject drawingCanvas;
    [SerializeField] TextMeshProUGUI pointerPositionTMPro;
    [SerializeField] public PointerController pointerController;
    [SerializeField] public ClickerController clickerController;

    //======================================================================
    Canvas mainCanvas;
    public Vector3 drawingCanvasBackgroundLBCorner;

    private void Start()
    {
        mainCanvas = gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        pointerPositionTMPro.text = ("Pointer [ " + Input.mousePosition.x + " , " + Input.mousePosition.y + " ]");
    }

    public void RedrawCanvas(Vector3 mousePosition, float currentScale, float previousScale)
    {
        Vector3 mouse2centerOffset = drawingCanvas.transform.position - mousePosition;
        //Debug.Log("m2cOffset: " + mouse2centerOffset);
        drawingCanvas.transform.position = mousePosition + (mouse2centerOffset / previousScale) * (currentScale);
        drawingCanvas.transform.localScale = Vector3.one * currentScale;
    }

    public void ResetCanvas()
    {
        ResetCanvasPosition();
        ResetCanvasScale();
    }

    public void ResetCanvasPosition()
    {
        RectTransform mainRect = mainCanvas.GetComponent<RectTransform>();
        drawingCanvas.transform.position = new Vector3(mainRect.rect.width / 2, mainRect.rect.height / 2, 0);
    }

    public void ResetCanvasScale()
    {
        GameManager.ins.Zoom = 1f;
        drawingCanvas.transform.localScale = Vector3.one;
    }

    public void ClearPointsLabels()
    {
        foreach (PointLabel label in clickerController.labelsContainer.GetComponentsInChildren<PointLabel>())
            Destroy(label.gameObject);
    }
}
