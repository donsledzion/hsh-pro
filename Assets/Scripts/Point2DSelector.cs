using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Selector2D;

public class Point2DSelector : MonoBehaviour
{

    [SerializeField] protected float _pointSnapDistance = 10f;
    [SerializeField] protected GameObject dotPrefab;

    protected GameObject _hoveringDotInstance;
    protected GameObject _selectingDotInstance;
    protected Vector2 _hoveredPoint;
    protected Vector2 _selectedPoint;

    protected void SelectPoint(Vector2 point, Color selectColor)
    {
        if (_hoveringDotInstance != null && (point == _hoveredPoint)/* && (point != _selectedPoint)*/)
        {
            if (_selectingDotInstance == null)
                _selectingDotInstance = Instantiate(dotPrefab);
            _selectingDotInstance.transform.SetParent(transform);
            _selectingDotInstance.transform.localPosition = point;
            _selectingDotInstance.transform.localScale = 5 * Vector3.one;
            _selectingDotInstance.GetComponent<Image>().color = selectColor;
            _selectedPoint = point;
        }
    }


    protected Vector2 HoverPoint(Vector2 point, Color hoverColor)
    {
        if (point != null && point != _hoveredPoint)
        {
            if (point != new Vector2(0f, 0f))
            {
                if (_hoveringDotInstance == null)
                    _hoveringDotInstance = Instantiate(dotPrefab);
                _hoveringDotInstance.transform.SetParent(transform);
                _hoveringDotInstance.transform.localPosition = point;
                _hoveringDotInstance.transform.localScale = 5 * Vector3.one;
                _hoveringDotInstance.GetComponent<Image>().color = hoverColor;
                _hoveredPoint = point;
            }
            else
            {
                UnhoverPoint();
            }
        }
        return _hoveredPoint;
    }

    protected void UnhoverPoint()
    {
        _hoveredPoint = new Vector2(0f, 0f);
        Destroy(_hoveringDotInstance);
    }

    protected virtual Point2DInfo ClosestPoint(Vector2 mouseInput)
    {
        Point2DInfo[] points = Drawing2DController.ins.CurrentStoreyInfoPoints;        
        Point2DInfo closestPoint = new Point2DInfo();
        float maxDist = _pointSnapDistance;
        foreach (Point2DInfo point in points)
        {
            if ((point.Point - mouseInput).magnitude < maxDist)
            {
                maxDist = (point.Point - mouseInput).magnitude;
                closestPoint = point;
            }
        }
        return closestPoint;
    }

}
