using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static StoreyPointsCollector;
using System;

public class WallCornerDebugger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _wallConnectionCount;

    public void SetConnectionsCount(int count)
    {
        _wallConnectionCount.text = count.ToString();   
    }

    public void SetDebugger(ConnectorPoint point)
    {
        SetConnectionsCount(point.sections.Count);
        SetPosition(point);
    }

    private void SetPosition(ConnectorPoint point)
    {
        transform.localPosition = point.Point;
    }
}
