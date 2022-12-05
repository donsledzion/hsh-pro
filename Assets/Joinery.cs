using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joinery : MonoBehaviour
{
    [SerializeField] JoineryType _joineryType;

    public JoineryType JoineryType => _joineryType;

    
}

public enum JoineryType
{
    Window,
    Door,
}
