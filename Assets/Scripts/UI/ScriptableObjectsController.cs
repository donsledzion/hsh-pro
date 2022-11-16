using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GaleryScriptableObjects", menuName = "ScriptableObjects/Gallery-Item")]
public class ScriptableObjectsController : ScriptableObject
{
    [SerializeField] public Sprite imagePreview;
    [SerializeField] public GameObject prefab;
    [SerializeField] public Material material;
    [SerializeField] public TextAsset Description;
    [SerializeField] public string name;
    [SerializeField] public string collectionName;
    [SerializeField] public float tiling_x;
    [SerializeField] public float tiling_y;
}
