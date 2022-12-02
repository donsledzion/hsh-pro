using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallCornerDebugger : MonoBehaviour
{
    TextMeshProUGUI _wallConnectionCount;


    public void SetConnectionsCount(int count)
    {
        _wallConnectionCount.text = count.ToString();   
    }
}
