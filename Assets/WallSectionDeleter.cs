using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class WallSectionDeleter : MonoBehaviour
{
    public static void DeleteSection(WallSection section)
    {
        Wall sectionsWall;
        if(section == null)
        {
            Debug.LogWarning("No section to delete");
            return;
        }
        Storey currentStorey = GameManager.ins.Building.CurrentStorey;

        foreach (Wall wall in currentStorey.Walls)
        {
            foreach (WallSection wallSection in wall.WallSections)
            {
                if(wallSection == section)
                {
                    sectionsWall = wall;
                    if (IsSectionOnWallsEdge(section, sectionsWall))
                        DeleteEdgeSection(section, sectionsWall);
                    else if (DeleteMidSection(section, sectionsWall)) return;
                }
            }
            if (wall.WallSections.Length < 1)
            {
                currentStorey.RemoveWall(wall);
                Drawing2DController.ins.RedrawCurrentStorey();
                break;
            }                
        }
    }

    public static bool IsSectionOnWallsEdge(WallSection section, Wall wall)
    {
        return ((section.StartPoint.Position == wall.Points2D[0])
            || (section.EndPoint.Position == wall.Points2D[0])
            || (section.StartPoint.Position == wall.Points2D[wall.Points2D.Length - 1])
            || (section.EndPoint.Position == wall.Points2D[wall.Points2D.Length - 1])
            ) ;
    }

    static bool DeleteMidSection(WallSection section, Wall wall)
    {
        if(wall.RemoveMidSection(section))
        {
            Drawing2DController.ins.RedrawCurrentStorey();
            return true;
        }
        Drawing2DController.ins.RedrawCurrentStorey();
        return false;
    }

    static void DeleteEdgeSection(WallSection section, Wall wall)
    {
        wall.RemoveEdgeSection(section);
        Drawing2DController.ins.RedrawCurrentStorey();
    }
}
