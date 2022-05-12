using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;

public class WallBuilder : MonoBehaviour
{

    [SerializeField] GameObject pointLabelPrefab;
    [SerializeField] public Transform labelsContainer;

    [SerializeField] UILineRenderer _uILineRenderer;
    [SerializeField] RectTransform _whiteboardBackground;
    List<Vector2> _pointList = new List<Vector2>();

    bool tmpLabelSpawned = false;
    GameObject tmpLabel;
    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-_whiteboardBackground.rect.width/2, -_whiteboardBackground.rect.height/2, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.ins.pointerOverUI)
        {
            SpawnPointLabel(Input.mousePosition, true);            
        }
        _uILineRenderer.DrawLive(Input.mousePosition - GameManager.ins.drawingCanvasBackgroundLBCorner/GameManager.ins.zoom);
        if (!tmpLabelSpawned)
        {
            tmpLabel = Instantiate(pointLabelPrefab, Input.mousePosition - GameManager.ins.drawingCanvasBackgroundLBCorner / GameManager.ins.zoom, pointLabelPrefab.transform.rotation);
            tmpLabel.transform.SetParent(labelsContainer);
            tmpLabelSpawned = true;
        }
        else
        {
            Vector2 lastPoint = _uILineRenderer.Points[_uILineRenderer.Points.Length-1];
            Vector2 lastVector = Vector2.right;
            if (_uILineRenderer.Points.Length > 2)
            {
                lastPoint = _uILineRenderer.Points[_uILineRenderer.Points.Length - 2];
                lastVector = _uILineRenderer.Points[_uILineRenderer.Points.Length - 2] - _uILineRenderer.Points[_uILineRenderer.Points.Length - 3]; ;
            }
            
            Vector2 cursorPos = Input.mousePosition - GameManager.ins.drawingCanvasBackgroundLBCorner / GameManager.ins.zoom;
            Vector2 currentVector = cursorPos - lastPoint;

            float angle = Vector2.Angle(currentVector, lastVector);

            tmpLabel.GetComponent<PointLabel>().SetLabelText(Mathf.Round((cursorPos-lastPoint).magnitude) + " [cm] |  " +Mathf.Round(angle*10)/10f );
            tmpLabel.transform.position = Input.mousePosition/* - GameManager.ins.drawingCanvasBackgroundLBCorner / GameManager.ins.zoom*/;
        }
    }

    public void SpawnPointLabel(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);

        GameObject tempScaler = new GameObject("TempScaler");
        label.transform.SetParent(tempScaler.transform);
        tempScaler.transform.localScale = Vector3.one * GameManager.ins.zoom;
        label.transform.SetParent(transform.root);
        label.transform.position = position;
        label.transform.SetParent(labelsContainer);


        Destroy(tempScaler);

        if (localPosition == true)
        {
            Vector3 ovcPos = (position - GameManager.ins.drawingCanvasBackgroundLBCorner) / GameManager.ins.zoom;
            _uILineRenderer.AddPoint(ovcPos);
            _uILineRenderer.LineThickness += 0.1f;
            _uILineRenderer.LineThickness -= 0.1f;

            label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10 * ovcPos.x)) / 10f + " , " + ((int)(10 * ovcPos.y)) / 10f + " ]");
        }
    }
}
