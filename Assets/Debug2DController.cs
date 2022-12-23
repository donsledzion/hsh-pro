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
    [SerializeField] GameObject _debugAlternativePointInstance;
    [SerializeField] GameObject _positionLabelPrefab;
    GameObject _sptccLabelInstance;
    PointLabel _sptccLabel;
    GameObject _alternativeLabelInstance;
    PointLabel _alternativeLabel;
    GameObject _cursorLabelInstance;
    PointLabel _cursorLabel;

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

    Vector2 AlternativeScreenPointToCanvasCoords(Vector2 input)
    {
        Vector2 outCoords = new Vector2();
        _debugAlternativePointInstance.transform.position = input;
        _debugAlternativePointInstance.transform.SetParent(transform);

        return _debugAlternativePointInstance.transform.localPosition;
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
        if (_debugAlternativePointInstance == null)
        {
            _debugAlternativePointInstance = Instantiate(_dotGridPrefab, transform);
            _debugAlternativePointInstance.GetComponent<Image>().color = Color.gray;
            _debugScreenPointToCanvasInstance.transform.localScale = Vector3.one * 3f;
        }
        if(_sptccLabelInstance == null)
        {
            _sptccLabelInstance = Instantiate(_positionLabelPrefab, transform);
            _sptccLabel = _sptccLabelInstance.GetComponent<PointLabel>();            
        }
        if(_alternativeLabelInstance == null)
        {
            _alternativeLabelInstance = Instantiate(_positionLabelPrefab, transform);
            _alternativeLabel = _alternativeLabelInstance.GetComponent<PointLabel>();
        }
        if(_cursorLabelInstance == null)
        {
            _cursorLabelInstance = Instantiate(_positionLabelPrefab, transform);
            _cursorLabel = _cursorLabelInstance.GetComponent<PointLabel>();
        }
        AlternativeScreenPointToCanvasCoords(Input.mousePosition);
        _alternativeLabel.Setup(AlternativeScreenPointToCanvasCoords(Input.mousePosition), Color.red);

        Vector2 mp = Input.mousePosition;

        //Vector2 inputData = new Vector2(mp.x*(1+resolutionMismatchCorrectX),mp.y*(1+resolutionMismatchCorrectY));

        _debugScreenPointToCanvasInstance.transform.localPosition = CanvasController.ScreenPointToCanvasCoords(mp,resolutionMismatchCorrectX,resolutionMismatchCorrectY);

        //_sptccLabel.Setup(CanvasController.ScreenPointToCanvasCoords(mp), Color.cyan);
        
    }
}
