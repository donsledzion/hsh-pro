using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DoorScriptableObjects", menuName = "ScriptableObjects/Door")]

public class DoorScriptableObjects : ScriptableObject
{
    
    public Sprite imagePreview;
    public GameObject prefab;
    public TextAsset Description;
    public string name;
    public string collectionName;
}
