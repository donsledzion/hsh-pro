using UnityEngine;
using UnityEngine.UI;
using Walls2D;

public class InsertWindow : Selector2D
{

    public static InsertWindow Instance { get; private set; }
    
    [SerializeField] GameObject _windowPrefab;
    [SerializeField] float _minOffsetFromEdge;
    GameObject _windowInstance;
    Window2DPrefab _windowToggler;

    Vector2 _snappedPoint;

    public float WindowWidth => float.Parse(_windowWidthInput.text, System.Globalization.NumberStyles.Float);
    public float WindowHeight => float.Parse(_windowHeightInput.text, System.Globalization.NumberStyles.Float);
    public float WindowsillElevation => float.Parse(_windowsillElevationInput.text, System.Globalization.NumberStyles.Float);

    [SerializeField] TMPro.TMP_InputField _windowHeightInput;
    [SerializeField] TMPro.TMP_InputField _windowWidthInput;
    [SerializeField] TMPro.TMP_InputField _windowsillElevationInput;

    [SerializeField] bool _freeToFitIn = false;
    Vector3 _startWindowEdge;
    Vector3 _endWindowEdge;

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
                _windowInstance = Instantiate(_windowPrefab, transform);
                _windowToggler = _windowInstance.GetComponent<Window2DPrefab>();
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
                _freeToFitIn = ValidatePosition();

                float radAngle = Mathf.PI * sectionAngle / 180f;
                Vector3 offset = new Vector3(
                    (WindowWidth+_minOffsetFromEdge)/2*Mathf.Cos(-radAngle),
                    (WindowWidth + _minOffsetFromEdge )/ 2*Mathf.Sin(-radAngle),
                    0f);

                _startWindowEdge = new Vector3(_snappedPoint.x + offset.x, _snappedPoint.y + offset.y, 0f);
                _endWindowEdge = new Vector3(_snappedPoint.x - offset.x, _snappedPoint.y - offset.y, 0f);

                _freeToFitIn = ValidatePosition();
            }
        }
        else
        {
            if (_windowInstance != null)
            {
                Destroy(_windowInstance);
                _windowToggler = null;
                

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

    bool ValidatePosition()
    {
        bool leftIsGood = _hoveredSection.PointLaysWithinSection(_startWindowEdge);
        bool rightIsGood = _hoveredSection.PointLaysWithinSection(_endWindowEdge);

        bool validationPassed = leftIsGood && rightIsGood;

        if (validationPassed && !_windowToggler.IsGood) _windowToggler.BeGood();
        else if (!validationPassed && _windowToggler.IsGood) _windowToggler.BeBad();

        return validationPassed;
    }


}
