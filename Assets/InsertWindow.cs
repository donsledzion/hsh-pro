using UnityEngine;
using Walls2D;

public class InsertWindow : Selector2D
{
    [SerializeField] GameObject _windowPrefab;
    GameObject _windowInstance;

    Vector2 _snappedPoint;

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
            }
            else
            {
                _windowInstance.transform.localPosition = _snappedPoint;
                _windowInstance.transform.rotation = Quaternion.identity;
                _windowInstance.transform.Rotate(Vector3.forward, -MathHelpers.VectorAzimuthDeg((_hoveredSection.EndPoint.Position - _hoveredSection.StartPoint.Position)));
            }
        }
        else
        {
            if (_windowInstance != null)
                Destroy(_windowInstance);
        }

        if (_windowInstance != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryFitWindow(_hoveredSection, _snappedPoint);
            }
        }
    }

    void TryFitWindow(WallSection wallSection, Vector2 position)
    {
        Debug.Log("Trying to fit window at :" + position);
        Debug.Log("wallSection :" + wallSection.ToString());
        Debug.Log("Section's wall :" + wallSection.Wall.ToString());

        Windowjamb jamb = new Windowjamb();
        Debug.Log("New Windowjamb.height :" + jamb.Height);
        jamb.SetAnchors(wallSection, position);
        Wall newWall = wallSection.Wall.InsertJambIntoSection(wallSection, jamb);
        WallSectionDeleter.DeleteSection(wallSection);
        GameManager.ins.Building.CurrentStorey.AddNewWall(newWall);
        Drawing2DController.ins.RedrawCurrentStorey();
    }
}
