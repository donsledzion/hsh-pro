using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionAlt : MonoBehaviour
{
    [SerializeField] GameObject wallBlock;
    public void Spatialize(WallSection wallSection)
    {
        float width = DefaultSettings.ins.LoadBareringWallWidth;
        float length = (wallSection.EndPoint.Position - wallSection.StartPoint.Position).magnitude;
        float height = DefaultSettings.ins.CeilingHeight;
        wallBlock.transform.localScale = new Vector3();
        wallBlock.transform.position = wallSection.StartPoint.Position;
    }
}
