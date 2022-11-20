using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class WallPlane : MonoBehaviour
{

    public Plane Plane => new Plane(transform.up, transform.position);

    [SerializeField] List<WallPlane> _collidingPlanes = new List<WallPlane>();

    public List<WallPlane> CollidingPlanes
    {
        get { return _collidingPlanes; }
    }

    public void DrawNormal()
    {
        Debug.DrawRay(transform.position,transform.up*1000,Color.blue);
    }

    public bool TryHitPlane(WallPlane wallPlane)
    {
        if (wallPlane == null || wallPlane == this) return false;
        Ray ray = new Ray(transform.position,transform.up);
        if(wallPlane.Plane.Raycast(ray, out float distance))
        {
            Debug.DrawRay(transform.position, ray.direction * distance, Color.green);
            return true;
        }

        return false;
    }
}
