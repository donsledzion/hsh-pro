using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallThicknessTemplate/* : MonoBehaviour*/
{
    [SerializeField] private string _name;
    [SerializeField] private float _thickness;

    public string Name => _name;
    public float Thickness => _thickness;
}
