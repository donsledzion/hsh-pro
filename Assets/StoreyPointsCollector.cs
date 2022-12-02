using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
using static StoreyPointsCollector;

public class StoreyPointsCollector : MonoBehaviour
{
    Vector2[] _storeyPoints;


    [ContextMenu("Collect All Points")]
    public List<WallPoint> CollectAllPoints(Storey storey)
    {
        List<WallPoint> allPoints = new List<WallPoint>();

        foreach(Wall wall in storey.Walls)
        {
            foreach(WallSection section in wall.WallSections)
            {                
                allPoints.Add(new WallPoint(wall, section, section.StartPoint));                
                allPoints.Add(new WallPoint(wall, section, section.EndPoint));                
            }
        }
        return allPoints;
    }


    public Vector2[] WallPointsListToVectorArray(List<WallPoint> wallPointsList)
    {
        List<Vector2> vectorList = new List<Vector2>();
        foreach (WallPoint wallPoint in wallPointsList)
            vectorList.Add(wallPoint.point);
        return vectorList.ToArray();
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
    public List<ConnectorPoint> ListConnectorPoints(Storey storey, int minConnections = 2)
    {
        List<ConnectorPoint> points = MinimumConnections(OnlyWallSectionConnectorPoints(FindConnectorPoints(CollectAllPoints(storey))),minConnections);
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

    List<ConnectorPoint> MinimumConnections(List<ConnectorPoint> allPoints, int minCount)
    {
        List<ConnectorPoint> wallSectionPoints = new List<ConnectorPoint>();

        foreach (ConnectorPoint point in allPoints)
        {
            if (point.sections.Count >= minCount)
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

        public float ThickestWallThickness()
        {
            foreach(WallSection section in sections)
            {
                if (section.Wall == null) return 30f;
                if (section.Wall.WallType == WallType.LoadBearing)
                    return DefaultSettings.ins.LoadBareringWallWidth;
                else if (section.Wall.WallType == WallType.Partition)
                    return DefaultSettings.ins.PartialWallWidth;
            }
            return 30f;
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

    public List<BasePoint> PointsAtPosition(Vector2 position, Type sectionType)
    {
        float tresholdDistnace = 5f;
        List<BasePoint> points = new List<BasePoint>();
        Storey currentStorey = GameManager.ins.Building.CurrentStorey;
        foreach(BasePoint point in currentStorey.BasePoints)
        {
            if((point.Position - position).magnitude <= tresholdDistnace)
            {
                points.Add(point);
            }
        }
        return points;
    }
}
