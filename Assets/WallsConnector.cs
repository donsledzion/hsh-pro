using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallsConnector : MonoBehaviour
{
    [SerializeField] public TillingAdjuster adjuster;

    List<WallSection> _sections = new List<WallSection>();

    [SerializeField] CollidingPlanesDetector planesDetector;

    [SerializeField] Material outterPlaneMaterial;
    [SerializeField] WallPlane outterPlane;

    Vector3 _outterPoint;

    Vector3 firstSectionToMe;
    Vector3 secondSectionToMe;
    
    Vector3 FurtherPoint(WallSection section)
    {
        if(section == null) return Vector3.zero;
        if (section.StartPoint.Position == new Vector2(transform.position.x,transform.position.z))
            return new Vector3(section.EndPoint.Position.x,transform.position.y,section.EndPoint.Position.y);
        return new Vector3(section.StartPoint.Position.x, transform.position.y, section.StartPoint.Position.y);
    }

    void HandleSectionsToMeVectors()
    {
        float tollerance = .5f;
        if(_sections.Count != 2 ) return;

        Vector2 A1 = _sections[0].StartPoint.Position;
        Vector2 A2 = _sections[0].EndPoint.Position;
        Vector2 B1 = _sections[1].StartPoint.Position;
        Vector2 B2 = _sections[1].EndPoint.Position;

        Vector2 V1 = new Vector2();
        Vector2 V2 = new Vector2();

        if ((A1 - B1).magnitude < tollerance)
        {
            V1 = (A1 - A2).normalized;
            V2 = (A1 - B2).normalized;
        }
        else if ((A1 - B2).magnitude < tollerance)
        {
            V1 = (A1 - A2).normalized;
            V2 = (A1 - B1).normalized;
        }
        else/*if ((A2 - B1).magnitude < tollerance)*/
        {
            V1 = (A2 - A1).normalized;
            V2 = (A2 - B2).normalized;
        }
        firstSectionToMe = new Vector3(V1.x,transform.position.y,V1.y)*100;
        secondSectionToMe = new Vector3(V2.x, transform.position.y, V2.y)*100;
    }

    private void Start()
    {
        _outterPoint = transform.position;
    }

    public List<WallSection> Sections
    {
        get { return _sections; }
        set { _sections = value; }
    }

    private void Update()
    {
        if(_sections.Count  == 2)
        {
            HandleSectionsToMeVectors();
            _outterPoint = transform.position + (firstSectionToMe + secondSectionToMe);
        }
        else
        {
            _outterPoint = transform.position;
        }
    }

    public void DelayedMaterialAssigning()
    {
        StartCoroutine("LookingForMaterialCor");
    }

    IEnumerator LookingForMaterialCor()
    {
        yield return new WaitForSeconds(.1f);
        adjuster.GetComponent<MeshRenderer>().material =  GetOutterPlaneMaterial();
        adjuster.SetTilling();
    }

    [ContextMenu("Get Outter Plane Material")]
    public Material GetOutterPlaneMaterial()
    {
        Debug.Log("Looking for material to cover the corner");
        foreach(WallPlane plane in planesDetector.Planes)
        {
            if(plane.Plane.GetSide(_outterPoint))
            {
                outterPlaneMaterial = plane.GetComponent<MeshRenderer>().material;
                outterPlane = plane;
                return outterPlaneMaterial;
            }
        }
        return adjuster.GetComponent<MeshRenderer>().material;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_outterPoint, 20f);
    }
}
