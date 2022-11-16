using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class StoreyPointsCollector : MonoBehaviour
{
    Vector2[] _storeyPoints;


    [ContextMenu("Collect All Points")]
    public List<WallPoint> CollectAllPoints()
    {
        List<WallPoint> allPoints = new List<WallPoint>();
        Storey storey = GameManager.ins.Building.CurrentStorey;

        foreach(Wall wall in storey.Walls)
        {
            foreach(WallSection section in wall.WallSections)
            {                
                allPoints.Add(new WallPoint(wall, section, section.StartPoint));                
                allPoints.Add(new WallPoint(wall, section, section.EndPoint));                
            }
        }
        Debug.Log("Found total: " + allPoints.Count + " points");
        return allPoints;
    }


    public List<ConnectorPoint> FindConnectorPoints(List<WallPoint> wallPoints)
    {
        List<ConnectorPoint> connectorPoints = new List<ConnectorPoint>();

        foreach(WallPoint wallPoint in wallPoints)
        {
            ConnectorPoint connectorPoint = ConnectorPointAlreadyExistsInList(wallPoint.point, connectorPoints);
            if(connectorPoint != null)
            {
                connectorPoint.sections.Add(wallPoint.wallSection);
            }
            else
            {
                connectorPoints.Add(new ConnectorPoint(wallPoint));
            }
        }

        return connectorPoints;
    }

    [ContextMenu("List connector points")]
    public List<ConnectorPoint> ListConnectorPoints()
    {
        List<ConnectorPoint> points = OnlyWallSectionConnectorPoints(FindConnectorPoints(CollectAllPoints()));
        Debug.Log("Found " + points.Count + " connection points");
        foreach(ConnectorPoint connectorPoint in points)
            Debug.Log(connectorPoint.Point + " Sections: " +connectorPoint.sections.Count);
        return points;
    }

    List<ConnectorPoint> OnlyWallSectionConnectorPoints(List<ConnectorPoint> allPoints)
    {
        List<ConnectorPoint> wallSectionPoints = new List<ConnectorPoint>();

        foreach(ConnectorPoint point in allPoints)
        {
            if(!ConnectorPointContainsJambSection(point))
                wallSectionPoints.Add(point);
        }

        return wallSectionPoints;
    }

    bool ConnectorPointContainsJambSection(ConnectorPoint connectorPoint)
    {
        foreach(WallSection section in connectorPoint.sections)
        {
            if (section.GetType() == typeof(Windowjamb) || section.GetType() == typeof(Doorjamb))
                return true;
        }
        return false;
    }


    ConnectorPoint ConnectorPointAlreadyExistsInList(Vector2 point, List<ConnectorPoint> points, float tollerance = 1f)
    {
        foreach (ConnectorPoint connectorPoint in points)
        {
            if (connectorPoint.Point == point || (connectorPoint.Point - point).magnitude < tollerance)
                return connectorPoint;
        }

        return null;
    }


    bool WallPointAlreadyExistsInList(BasePoint point, List<WallPoint> points, float tollerance = 0.1f)
    {
        foreach(WallPoint wallPoint in points)
        {
            if (wallPoint.point == point.Position)
                return true;
        }
        return false;
    }

    public class ConnectorPoint
    {
        public Vector2 Point;
        public List<WallSection> sections;

        public ConnectorPoint()
        {
            Point = new Vector2();  
            sections = new List<WallSection>();
        }

        public ConnectorPoint(WallPoint wallPoint)
        {
            Point = wallPoint.point;
            sections = new List<WallSection>();
            sections.Add(wallPoint.wallSection);
        }
    }

    public class WallPoint
    {
        public Vector2 point;
        public WallSection wallSection;
        public Wall wall;
        public WallsConnector connector;

        public WallPoint()
        {
            point = new Vector2();            
        }

        public WallPoint(Wall wall, WallSection section, BasePoint point)
        {
            this.wall = wall;
            this.wallSection = section;
            this.point = point.Position;
        }
    }
}
