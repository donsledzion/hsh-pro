using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMaterialController : MonoBehaviour
{
    public static CurrentMaterialController ins { get; private set; }

    [SerializeField] WallSurfaceSelector _wallSurfaceSelector;
    [SerializeField] FloorSurfaceSelector _floorSurfaceSelector;
    [SerializeField] Vector2 _floorMaterialTilling = new Vector2(100f,100f);

    public FloorSurfaceSelector FloorSurfaceSelector { get { return _floorSurfaceSelector; } }
    public WallSurfaceSelector WallSurfaceSelector { get { return _wallSurfaceSelector; } }

    public Vector2 FloorMaterialTilling { get { return _floorMaterialTilling; } set { _floorMaterialTilling = value; } }


    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }
}
