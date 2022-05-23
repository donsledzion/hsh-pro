using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallBuilder : MonoBehaviour
{
    List<WallSection> _wallSections = new List<WallSection>();
    [SerializeField] Drawing2DController _drawing2DController;

    

    public void AddWallSection()
    {
        Vector2[] allPoints = _drawing2DController.LinePoints;
        int allPointsCount = allPoints.Length;
        if (allPointsCount < 2) return;
        Vector2[] sectionPoints = { allPoints[allPointsCount - 2], allPoints[allPointsCount - 1] };
        WallSection section = new SectionStraight(sectionPoints);

        _wallSections.Add(section);
    }
}
