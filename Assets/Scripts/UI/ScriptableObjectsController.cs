using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GaleryScriptableObjects", menuName = "ScriptableObjects/Gallery-Item")]
public class ScriptableObjectsController : ScriptableObject
{
    public Sprite imagePreview;
    public GameObject prefab;
    public TextAsset Description;
    public string name;
    public string collectionName;
}
