using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] Canvas mainCanvas;
    [SerializeField] public GameObject drawingCanvas;
    [SerializeField] TextMeshProUGUI pointerPositionTMPro;
    [SerializeField] public PointerController pointerController;
    [SerializeField] public ClickerController clickerController;

    public Vector3 drawingCanvasBackgroundLBCorner;

    public float zoom;


    private void Update()
    {
        zoom = pointerController.outZoom;

        pointerPositionTMPro.text = ("Pointer [ " + Input.mousePosition.x + " , " + Input.mousePosition.y + " ]");
    }

    public void RedrawCanvas(Vector3 mousePosition, float currentScale, float previousScale)
    {
        Vector3 mouse2centerOffset = drawingCanvas.transform.position - mousePosition;
        Debug.Log("m2cOffset: " + mouse2centerOffset);
        drawingCanvas.transform.position = mousePosition + (mouse2centerOffset/previousScale)*(currentScale);
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
        drawingCanvas.transform.position = new Vector3(mainRect.rect.width/2, mainRect.rect.height/2,0);
    }

    public void ResetCanvasScale()
    {
        drawingCanvas.transform.localScale = Vector3.one;
    }

    public void ClearPointsLabels()
    {
        foreach (PointLabel label in clickerController.labelsContainer.GetComponentsInChildren<PointLabel>())
            Destroy(label.gameObject);
    }
}
