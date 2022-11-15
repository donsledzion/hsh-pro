using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlane : MonoBehaviour
{

    public Plane Plane => new Plane(transform.up, transform.position);

}
