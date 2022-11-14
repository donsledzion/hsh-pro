using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSerializer : MonoBehaviour
{
    [ContextMenu("Serialize Building")]
    void SerializeBuilding()
    {
        GameManager.ins.Building.SerializeToXML();
    }
}
