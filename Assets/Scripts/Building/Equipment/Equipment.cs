using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Equipment
{
    string _guid;
    string _assetName;
    string _bundleName;
    Vector3 _position;
    Vector3 _rotation;


    public string GUID
    {
        get { return _guid; }
        set { _guid = value; }  
    }
    public string AssetName
    {
        get { return _assetName; }
        set { _assetName = value; }
    }

    public string BundleName
    {
        get { return _bundleName; }
        set { _bundleName = value; }    
    }

    public Vector3 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public Vector3 Rotation
    {
        get { return _rotation; }
        set { _rotation = value; }
    }

    
    public Equipment()
    {
        _guid = "";
        _assetName = "";
        _bundleName = "";
        _position = Vector3.zero;
        _rotation = Vector3.zero;
    }

    public Equipment(string guid, string assetName, string bundleName, Vector3 position, Vector3 rotation)
    {
        _guid = guid;
        _assetName = assetName;
        _bundleName = bundleName;
        _position = position;
        _rotation = rotation;
    }
}
