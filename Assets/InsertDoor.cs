using UnityEngine;
using Walls2D;

public class InsertDoor : Selector2D
{
    [SerializeField] GameObject _doorPrefab;
    [SerializeField] float _minOffsetFromEdge;
    GameObject _doorInstance;

    Vector2 _snappedPoint;

    public float DoorWidth => float.Parse(_doorWidthInput.text, System.Globalization.NumberStyles.Float);
    public float DoorHeight => float.Parse(_doorHeightInput.text, System.Globalization.NumberStyles.Float);

    [SerializeField] TMPro.TMP_InputField _doorHeightInput;
    [SerializeField] TMPro.TMP_InputField _doorWidthInput;

    [SerializeField] bool _freeToFitIn = false;
    Vector3 _startSectionEdge;
    Vector3 _endSectionEdge;

    private void Start()
    {
        if (_minOffsetFromEdge <= 0)
            _minOffsetFromEdge = DefaultSettings.ins.LoadBareringWallWidth;
    }

    protected override void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        _hoveredSection = ClosestSection(mouseOverCanvas);
        if(_hoveredSection != null)
        {
            _snappedPoint = CastedPoint(_hoveredSection, mouseOverCanvas);
            HoverPoint(_snappedPoint,_hoverColor);
            if(_doorInstance == null)
            {
                if(ValidateInputs())
                {
                    _doorInstance = Instantiate(_doorPrefab, transform);
                    _validationToggler = _doorInstance.GetComponent<JambSectionValidationToggler>();
                }
            }
            else
            {
                float sectionAngle = MathHelpers.VectorAzimuthDeg(
                    (_hoveredSection.EndPoint.Position
                    - _hoveredSection.StartPoint.Position));

                _doorInstance.transform.localPosition = _snappedPoint;
                _doorInstance.transform.rotation = Quaternion.identity;
                _doorInstance.transform.Rotate(Vector3.forward,-sectionAngle);
                float doorSectionThickness = (_hoveredSection.Wall.WallType == WallType.LoadBearing 
                    ? DefaultSettings.ins.LoadBareringWallWidth 
                    : DefaultSettings.ins.PartialWallWidth);

                _doorInstance.transform.localScale = new Vector3(DrawingParametersController.ins.DoorWidth / 100f, 1.5f * doorSectionThickness / 30f, 1);


                float radAngle = Mathf.PI * sectionAngle / 180f;
                Vector3 offset = new Vector3(
                    (DoorWidth + _minOffsetFromEdge) / 2 * Mathf.Cos(-radAngle),
                    (DoorWidth + _minOffsetFromEdge) / 2 * Mathf.Sin(-radAngle),
                    0f);
                _startSectionEdge = new Vector3(_snappedPoint.x + offset.x, _snappedPoint.y + offset.y, 0f);
                _endSectionEdge = new Vector3(_snappedPoint.x - offset.x, _snappedPoint.y - offset.y, 0f);
                _freeToFitIn = ValidatePosition(_startSectionEdge, _endSectionEdge);
            }
        }
        else
        {
            if (_doorInstance != null)
            {
                Destroy(_doorInstance);
                _validationToggler = null;
            }
        }

        if(_doorInstance!= null)
        {            
            if(Input.GetMouseButtonDown(0))
            {
                if(_freeToFitIn)
                    TryFitDoors(_hoveredSection,_snappedPoint);
            }
        }            
    }

    private bool ValidateInputs()
    {
        bool valid = true;

        if (DoorWidth < DefaultSettings.ins.MinDoorWidth)
        {
            Debug.LogWarning("Door width given by input is less than minimal defined in settings: "
                + DoorWidth + " < " + DefaultSettings.ins.MinDoorWidth);
            valid = false;
        }
        if (DoorHeight < DefaultSettings.ins.MinDoorHeight)
        {
            Debug.LogWarning("Door height given by input is less than minimal defined in settings: "
                + DoorHeight + " < " + DefaultSettings.ins.MinDoorHeight);
            valid = false;
        }
        if ((DoorHeight) > (GameManager.ins.Building.CurrentStorey.Height - DefaultSettings.ins.CeilingThickness))
        {
            Debug.LogWarning("Door height is more than current storey height!");
            valid = false;
        }
        return valid;
    }

        void TryFitDoors(WallSection wallSection, Vector2 position)
    {
        Doorjamb jamb = new Doorjamb();

        jamb.SetDoorJambParameters(DoorHeight, DoorWidth);
        jamb.SetAnchors(wallSection, position, DoorWidth);
        Wall newWall = wallSection.Wall.InsertJambIntoSection(wallSection, jamb);
        WallSectionDeleter.DeleteSection(wallSection);
        GameManager.ins.Building.CurrentStorey.AddNewWall(newWall);
        Drawing2DController.ins.RedrawCurrentStorey();
    }
}
