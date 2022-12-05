using System;
using UnityEngine;
using Walls2D;

public class InsertWindow : Selector2D
{

    public static InsertWindow Instance { get; private set; }
    
    [SerializeField] GameObject _windowPrefab;
    [SerializeField] float _minOffsetFromEdge;
    GameObject _windowInstance;

    Vector2 _snappedPoint;

    public float WindowWidth => float.Parse(_windowWidthInput.text, System.Globalization.NumberStyles.Float);
    public float WindowHeight => float.Parse(_windowHeightInput.text, System.Globalization.NumberStyles.Float);
    public float WindowsillElevation => float.Parse(_windowsillElevationInput.text, System.Globalization.NumberStyles.Float);

    [SerializeField] TMPro.TMP_InputField _windowHeightInput;
    [SerializeField] TMPro.TMP_InputField _windowWidthInput;
    [SerializeField] TMPro.TMP_InputField _windowsillElevationInput;

    [SerializeField] bool _freeToFitIn = false;
    Vector3 _startSectionEdge;
    Vector3 _endSectionEdge;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (_minOffsetFromEdge <= 0)
            _minOffsetFromEdge = DefaultSettings.ins.LoadBareringWallWidth;
    }

    protected override void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        _hoveredSection = ClosestSection(mouseOverCanvas);
        if (_hoveredSection != null)
        {
            _snappedPoint = CastedPoint(_hoveredSection, mouseOverCanvas);
            HoverPoint(_snappedPoint,_hoverColor);
            if (_windowInstance == null)
            {
                if(ValidateInputs())
                {
                    _windowInstance = Instantiate(_windowPrefab, transform);
                    _validationToggler = _windowInstance.GetComponent<JambSectionValidationToggler>();
                }
            }
            else
            {
                float sectionAngle = MathHelpers.VectorAzimuthDeg(
                        (_hoveredSection.EndPoint.Position
                        - _hoveredSection.StartPoint.Position));

                _windowInstance.transform.localPosition = _snappedPoint;
                _windowInstance.transform.rotation = Quaternion.identity;
                _windowInstance.transform.Rotate(
                    Vector3.forward,
                    -sectionAngle);
                float windowSectionThickness = 
                    (_hoveredSection.Wall.WallType == WallType.LoadBearing
                    ? DefaultSettings.ins.LoadBareringWallWidth
                    : DefaultSettings.ins.PartialWallWidth);
                _windowInstance.transform.localScale = 
                    new Vector3(
                        DrawingParametersController.ins.WindowWidth/100f,
                        1.5f*windowSectionThickness/30f,
                        _windowInstance.transform.localScale.z);

                float radAngle = Mathf.PI * sectionAngle / 180f;
                Vector3 offset = new Vector3(
                    (WindowWidth+_minOffsetFromEdge)/2*Mathf.Cos(-radAngle),
                    (WindowWidth + _minOffsetFromEdge )/ 2*Mathf.Sin(-radAngle),
                    0f);

                _startSectionEdge = new Vector3(_snappedPoint.x + offset.x, _snappedPoint.y + offset.y, 0f);
                _endSectionEdge = new Vector3(_snappedPoint.x - offset.x, _snappedPoint.y - offset.y, 0f);

                _freeToFitIn = ValidatePosition(_startSectionEdge, _endSectionEdge);
            }
        }
        else
        {
            if (_windowInstance != null)
            {
                Destroy(_windowInstance);
                _validationToggler = null;
            }
        }

        if (_windowInstance != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_freeToFitIn)
                    TryFitWindow(_hoveredSection, _snappedPoint);
            }
        }
    }

    private bool ValidateInputs()
    {
        bool valid = true;

        if (WindowWidth < DefaultSettings.ins.MinWindowWidth)
        {
            Debug.LogWarning("Window width given by input is less than minimal defined in settings: "
                + WindowWidth + " < " + DefaultSettings.ins.MinWindowWidth);
            valid = false;
        }
        if(WindowHeight < DefaultSettings.ins.MinWindowHeight)
        {
            Debug.LogWarning("Window height given by input is less than minimal defined in settings: "
                + WindowHeight + " < " + DefaultSettings.ins.MinWindowHeight);
            valid = false;
        }
        if((WindowHeight+WindowsillElevation) > (GameManager.ins.Building.CurrentStorey.Height - DefaultSettings.ins.CeilingThickness ))
        {
            Debug.LogWarning("Window height with given elevation is more than current storey height!");
            valid = false;
        }

        return valid;
    }

    void TryFitWindow(WallSection wallSection, Vector2 position)
    {
        Windowjamb jamb = new Windowjamb();        
        jamb.SetWindowJambParameters(WindowHeight, WindowWidth, WindowsillElevation);
        jamb.SetAnchors(wallSection, position, WindowWidth);

        Wall newWall = wallSection.Wall.InsertJambIntoSection(wallSection, jamb);
        WallSectionDeleter.DeleteSection(wallSection);
        GameManager.ins.Building.CurrentStorey.AddNewWall(newWall);
        Drawing2DController.ins.RedrawCurrentStorey();
    }


}
