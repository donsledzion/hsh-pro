using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug2DController : MonoBehaviour
{
    [SerializeField] GameObject _dotGridPrefab;


    //============== LB Cornner ======================================
    [SerializeField] bool _debugLBCorner = false;
    [SerializeField] GameObject _debugLBCornerDotInstance;
    [SerializeField] bool _lbCornerLocalPosition = true;

    //============== Screen Point To Canvas ==========================
    [SerializeField] bool _debugScreenPointToCanvas = false;
    [SerializeField] GameObject _debugScreenPointToCanvasInstance;

    [Range(0f, 2f)]
    [SerializeField] float resolutionMismatchCorrectX = 1;

    [Range(0f, 2f)]
    [SerializeField] float resolutionMismatchCorrectY = 1;


    private void Update()
    {
        if (_debugLBCorner)
            DebugLBCorner();
        if(_debugScreenPointToCanvas)
            DebugScreenPointToCanvas();
    }

    void DebugLBCorner()
    {
        if(_debugLBCornerDotInstance == null)
        {
            _debugLBCornerDotInstance = Instantiate(_dotGridPrefab, transform);
            _debugLBCornerDotInstance.GetComponent<Image>().color = Color.green;
            _debugLBCornerDotInstance.transform.localScale = Vector3.one * 3f;

        }
        if(_lbCornerLocalPosition)
            _debugLBCornerDotInstance.transform.localPosition = GameManager.ins.DrawingCanvasBackgroundLBCorner;
        else
            _debugLBCornerDotInstance.transform.position = GameManager.ins.DrawingCanvasBackgroundLBCorner;
    }


    void DebugScreenPointToCanvas()
    {
        if (_debugScreenPointToCanvasInstance == null)
        {
            _debugScreenPointToCanvasInstance = Instantiate(_dotGridPrefab, transform);
            _debugScreenPointToCanvasInstance.GetComponent<Image>().color = Color.cyan;
            _debugScreenPointToCanvasInstance.transform.localScale = Vector3.one * 3f;
        }

        Vector2 mp = Input.mousePosition;

        //Vector2 inputData = new Vector2(mp.x*(1+resolutionMismatchCorrectX),mp.y*(1+resolutionMismatchCorrectY));

        _debugScreenPointToCanvasInstance.transform.localPosition = CanvasController.ScreenPointToCanvasCoords(mp,resolutionMismatchCorrectX,resolutionMismatchCorrectY);
    }
}
