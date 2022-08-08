using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector2D : MonoBehaviour
{
    [SerializeField] Color _hoverColor = Color.red;
    [SerializeField] Color _selectColor = Color.blue;
    private void OnEnable()
    {
        StoreyCreator.ins.SwitchOffAll();
    }

    private void Update()
    {
        
    }
}
