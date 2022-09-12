using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPrefabWindow : MonoBehaviour
{
    [SerializeField] GameObject _doorPrefab;

    public GameObject DoorPrefab { get { return _doorPrefab; } set { _doorPrefab = value; } }
}
