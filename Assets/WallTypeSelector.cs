using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
using TMPro;

public class WallTypeSelector : MonoBehaviour
{
    [SerializeField] List<WallType> wallTypes = new List<WallType>();

    public void SetCurrentWallType()
    {
        int index = gameObject.GetComponent<TMP_Dropdown>().value; 
        Drawing2DController.ins.CurrentWallType = wallTypes[index];
    }    
}
