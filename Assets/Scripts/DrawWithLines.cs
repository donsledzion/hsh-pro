using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public abstract class DrawWithLines : DrawOnCanvas
{
    protected bool _oneClick = false;
    bool _timerRunning;
    float _timerForDoubleClick;
    bool _doubleClick = false;
    [SerializeField] float _delay=.2f;


    protected DynamicInputController _dynamicInputController;
    protected bool _waitingForConfirm = false;
    public bool IsDrawing { get; protected set; }

    protected override void Start()
    {
        base.Start();
        _dynamicInputController = DynamicInputController.ins;
    }

    protected override void Update()
    {
        _doubleClick = false;
        if (Input.GetMouseButtonDown(0))
        {
            if (!_oneClick)
            {
                _oneClick = true;
                _timerForDoubleClick = Time.time;
            }
            else
            {
                _oneClick = false;
                _doubleClick = true;
            }
        }
        if (_oneClick)
        {
            if ((Time.time - _timerForDoubleClick) > _delay)
            {
                _oneClick = false;
            }
        }

            base.Update();
        if(!_waitingForConfirm)
        {
            if (GameManager.ins.PointerOverUI && IsDrawing)
                _drawing2DController.DrawLive(pointerPosition);

            if (Input.GetMouseButtonDown(0) && GameManager.ins.PointerOverUI)
            {
                if (!IsDrawing)
                    IsDrawing = true;            
                _drawing2DController.AddLinePoint(pointerPosition, true,false);
                HandleClick();
            }
        

            if ((Input.GetMouseButtonDown(1) || _doubleClick) && GameManager.ins.PointerOverUI)
            {
                BreakLine();
            }
            if(GameManager.ins.DynamicDimensions)
                _dynamicInputController.DynamicInput();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                int pointsCount = _drawing2DController.LinePointsCount;
                Vector2 lastPoint = _drawing2DController.LinePoints[pointsCount - 1];
                if (_dynamicInputController.DynamicInputLength > 0 && lastPoint != Vector2.zero)
                {
                    _drawing2DController.ApplyDynamicInput(pointerPosition);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BreakLine();
            }
        }
    }

    protected abstract void BreakLine();
    protected abstract void HandleClick();


    protected Vector2[] FilterPoints(Vector2[] sourcePoints)
    {
        return FilterFirstLastPoint(sourcePoints);
    }
    Vector2[] FilterFirstLastPoint(Vector2[] sourcePoints)
    {
        Vector2[] newPoints = new Vector2[0];
        bool isFiltered = false;
        if (sourcePoints[0] == sourcePoints[sourcePoints.Length - 1])
        {
            newPoints = new Vector2[sourcePoints.Length - 1];
            for (int i = 0; i < newPoints.Length; i++)
            {
                newPoints[i] = sourcePoints[i];
            }
            isFiltered = true;
        }
        Debug.Log("NewPoints.Count: " + newPoints.Length);
        return isFiltered ? newPoints : sourcePoints;
    }
    public static Vector2[] EnsureLineIsClosed(Vector2[] points, float tollerance)
    {
        if ((points[0] - points[points.Length - 1]).magnitude > tollerance)
        {
            List<Vector2> newPoints = new List<Vector2>(points);
            newPoints.Add(points[0]);
            points = newPoints.ToArray();
        }
        return points;
    }

    protected virtual void OnDisable()
    {
        BreakLine();   
    }
}
