using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Walls2D;

public class CollidingPlanesDetector : MonoBehaviour
{

    [SerializeField] List<WallPlane> _planes = new List<WallPlane>();

    public List<WallPlane> Planes
    {
        get { return _planes; }
    }

    private void Update()
    {

        Debug.Log("My collidingg planes count: " + _planes.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Some trigger entered (tag): " + other.tag);
        if (other.gameObject.CompareTag("WallSnapSurface"))
        {
            WallPlane plane = other.GetComponent<WallPlane>();
            if (!_planes.Contains(plane))
                _planes.Add(plane);
        }
    }
}
