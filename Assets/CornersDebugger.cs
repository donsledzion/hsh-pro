using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CornersDebugger : MonoBehaviour
{


    [SerializeField] Color _startDebuggerColorL = Color.red;
    [SerializeField] Color _startDebuggerColorR = Color.blue;

    [SerializeField] TextMeshProUGUI _left;
    [SerializeField] TextMeshProUGUI _right;

    [SerializeField] float _offset = 40f;

    public void SetLeft(string key, float value, Vector2 position)
    {
        _left.transform.localPosition = position + Vector2.up * _offset;
        _left.text = key + ": " + (int)value;
    }
    
    public void SetRight(string key, float value, Vector2 position)
    {
        _right.transform.localPosition  = position + Vector2.down * _offset;
        _right.text = key + ": " + (int)value;
    }
}
