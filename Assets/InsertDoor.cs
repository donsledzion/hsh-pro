using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class InsertDoor : Selector2D
{
    [SerializeField] GameObject _doorPrefab;
    GameObject _doorInstance;

    Vector2 _snappedPoint;

    protected override void Update()
    {
        Vector2 mouseOverCanvas = CanvasController.ScreenPointToCanvasCoords(Input.mousePosition);
        _hoveredSection = ClosestSection(mouseOverCanvas);
        if(_hoveredSection != null)
        {
            _snappedPoint = CastedPoint(_hoveredSection, mouseOverCanvas);
            HoverPoint(_snappedPoint);
            if(_doorInstance == null)
            {
                _doorInstance = Instantiate(_doorPrefab, transform);
            }
            else
            {
                _doorInstance.transform.localPosition = _snappedPoint;
                _doorInstance.transform.rotation = Quaternion.identity;
                _doorInstance.transform.Rotate(Vector3.forward,-MathHelpers.VectorAzimuthDeg((_hoveredSection.EndPoint.Position-_hoveredSection.StartPoint.Position)));
            }
        }
        else
        {
            if (_doorInstance != null)
                Destroy(_doorInstance);
        }


        if(_doorInstance!= null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                TryFitDoors(_hoveredSection,_snappedPoint);
            }
        }            
    }

    void TryFitDoors(WallSection wallSection, Vector2 position)
    {
        Debug.Log("Trying to fit door at :" + position);
        Jamb jamb = new Jamb();
        jamb.SetAnchors(wallSection, position);
        Wall newWall = wallSection.Wall.InsertJambIntoSection(wallSection, jamb);
        WallSectionDeleter.DeleteSection(wallSection);
        GameManager.ins.Building.CurrentStorey.AddNewWall(newWall);
        Drawing2DController.ins.RedrawCurrentStorey();
    }

    Vector2 CastedPoint(WallSection section, Vector2 mouseInput)
    {
        return MathHelpers.PointCastOnLine(section.StartPoint.Position,section.EndPoint.Position,mouseInput);
    }
}
