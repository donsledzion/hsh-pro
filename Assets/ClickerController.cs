using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerController : MonoBehaviour
{
    [SerializeField] GameObject pointLabelPrefab;
    [SerializeField] public Transform labelsContainer;
    [SerializeField] GameManager gameManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)/*&&gameManager.pointerOverUI*/)
        {
            
            SpawnPointLabel(Input.mousePosition, true);
        }
            
        CanvasStats();
    }


    public void SpawnPointLabel(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);
        label.transform.SetParent(labelsContainer);
        //label.transform.localScale = Vector3.one * gameManager.pointerController.outZoom;

        if (localPosition==true)
        {
            Vector3 ovcPos = (position - gameManager.drawingCanvasBackgroundLBCorner)/gameManager.zoom;

            label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10*ovcPos.x))/10f + " , " + ((int)(10*ovcPos.y))/10f + " ]");
        }
    }

    void CanvasStats()
    {

        float zoom = gameManager.pointerController.outZoom;
        GameObject drawingCanvas = gameManager.drawingCanvas;
        RectTransform drawingCanvasBg = drawingCanvas.transform.Find("Background").GetComponent<RectTransform>();
        Debug.Log("Canvas center at: " + drawingCanvas.transform.position.x + " , " + drawingCanvas.transform.position.y);
        Debug.Log("Canvas dimensions:" +
            " w=" + drawingCanvasBg.rect.width +
            " , h=" + drawingCanvasBg.rect.height);
        Debug.Log("Canvas dimensions (with scale):" +
            " w=" + drawingCanvasBg.rect.width*zoom +
            " , h=" + drawingCanvasBg.rect.height*zoom);
    }

}
