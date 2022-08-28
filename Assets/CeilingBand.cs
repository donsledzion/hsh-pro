using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CeilingBand : MonoBehaviour
{
    Ceiling _ceiling;    
    [SerializeField] float _overlappingOffset = 0.01f;
    [SerializeField] GameObject _ceilingBandPrefab;

    public void Spatialize()
    {
        for(int i = 0; i < _ceiling.Points.Length; i++)
        {
            GenerateBand(i);
        }
    }

    void GenerateBand(int i)
    {
        GameObject bandObject = Instantiate(_ceilingBandPrefab);
        bandObject.transform.SetParent(gameObject.transform);
        CeilingBandPlane plane = bandObject.GetComponent<CeilingBandPlane>();
        Vector3[] corners = new Vector3[4];
        corners[0] = new Vector3(_ceiling.Points[i].x, _ceiling.TopLevel-_ceiling.Thickness , _ceiling.Points[i].y);
        corners[1] = new Vector3(_ceiling.Points[i].x, _ceiling.TopLevel+_overlappingOffset, _ceiling.Points[i].y);
        if(i<_ceiling.Points.Length-1)
        {
            corners[2] = new Vector3(_ceiling.Points[i + 1].x, _ceiling.TopLevel - _ceiling.Thickness, _ceiling.Points[i + 1].y);
            corners[3] = new Vector3(_ceiling.Points[i + 1].x, _ceiling.TopLevel + _overlappingOffset, _ceiling.Points[i + 1].y);
        }
        else if(i == _ceiling.Points.Length - 1)
        {
            corners[2] = new Vector3(_ceiling.Points[0].x, _ceiling.TopLevel - _ceiling.Thickness, _ceiling.Points[0].y);
            corners[3] = new Vector3(_ceiling.Points[0].x, _ceiling.TopLevel + _overlappingOffset, _ceiling.Points[0].y);
        }
        
        plane.GenerateMesh(corners);
    }

    public void SetParameters(Ceiling ceiling)
    {
        _ceiling = ceiling;
    }
}
