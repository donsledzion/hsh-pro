using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Walls2D
{
    [Serializable]
    public class Wall
    {
        WallSection[] _wallSections;
        //WallType _wallType;
        float _thickness;
        int _orderInStorey;

        public Wall(){}

        public Wall(List<WallSection> wallSections)
        {
            _wallSections = wallSections.ToArray();
            PutSectionsInOrder();
            _orderInStorey = -1;
            _thickness = wallSections[0].Thickness;
        }

        public Wall(Vector2[] linePoints, float thickness)
        {
            _thickness = thickness;
            if (linePoints.Length < 2) return;

            List<WallSection> newSections = new List<WallSection>();

            for(int i = 0; i < linePoints.Length-1; i++)
            {
                newSections.Add(new SectionStraight(linePoints[i], linePoints[i + 1]));
            }
            _wallSections = newSections.ToArray();
            AssignSections();
            PutSectionsInOrder();
            _orderInStorey = -1;
        }

        public WallSection[] WallSections
        {
            get { return _wallSections; }
            set { _wallSections = value; }
        }

        /*public WallType WallType
        {
            get { return _wallType; }
            set { _wallType = value; }
        }*/

        public float Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }


        public int OrderInStorey
        {
            get { return _orderInStorey; }
            set { _orderInStorey = value; }
        }

        public Vector2[] Points2D
        {
            get
            {
                List<Vector2> points = new List<Vector2>();

                if (WallSections.Length == 0) return points.ToArray();
                points.Add(WallSections[0].StartPoint.Position);
                foreach(WallSection section in WallSections)
                {
                    points.Add(section.EndPoint.Position);
                }
                return points.ToArray();
            }
            
        }

        internal void PutSectionsInOrder()
        {
            int index = 0;
            foreach(WallSection section in WallSections)
            {
                section.OrderInWall = index;
                index++;
            }
        }

        internal bool RemoveMidSection(WallSection section)
        {
            if (WallSectionDeleter.IsSectionOnWallsEdge(section, this)) return false;
            int sectionStartIndex = -1;
            int sectionEndIndex = -1;
            for(int i = 0; i < this.Points2D.Length; i++)
            {
                if (Points2D[i] == section.StartPoint.Position) sectionStartIndex = i;
                if (Points2D[i] == section.EndPoint.Position) sectionEndIndex = i;
            }
            List<Vector2> firstWallPoints = new List<Vector2>();
            List<Vector2> secondWallPoints = new List<Vector2>();
            bool sectionSpotted = false;
            
            foreach(Vector2 point in Points2D)
            {
                if (!sectionSpotted)
                    firstWallPoints.Add(point);
                else
                    secondWallPoints.Add(point);
                if (sectionStartIndex < sectionEndIndex)
                {
                    if (point == section.StartPoint.Position)
                        sectionSpotted = true;
                }
                else if (sectionEndIndex < sectionStartIndex)
                {
                    if (point == section.EndPoint.Position)
                        sectionSpotted = true;
                }
            }
            Wall firstWall = new Wall(firstWallPoints.ToArray(),section.Thickness);
            Wall secondWall = new Wall(secondWallPoints.ToArray(),section.Thickness);
            GameManager.ins.Building.CurrentStorey.AddNewWall(firstWall);
            GameManager.ins.Building.CurrentStorey.AddNewWall(secondWall);
            return GameManager.ins.Building.CurrentStorey.RemoveWall(this);            
        }

        public Wall InsertJambIntoSection(WallSection wallSection, Jamb jamb)
        {
            if(_wallSections.Contains(wallSection))
            {
                Debug.Log("Inserting (inside if)");
                WallSection duplicateSectionA = wallSection.Clone();
                WallSection duplicateSectionB = wallSection.Clone();
                List<WallSection> newSections = new List<WallSection>();

                Vector2 closerAnchor = (jamb.StartPoint.Position - wallSection.StartPoint.Position).magnitude < (jamb.EndPoint.Position - wallSection.StartPoint.Position).magnitude ? jamb.StartPoint.Position : jamb.EndPoint.Position;
                Vector2 furtherAnchor = closerAnchor == jamb.StartPoint.Position ? jamb.EndPoint.Position : jamb.StartPoint.Position;

                duplicateSectionA.EndPoint.Position = closerAnchor;
                duplicateSectionB.StartPoint.Position = furtherAnchor;
                
                newSections.Add(duplicateSectionA);
                newSections.Add(jamb);
                newSections.Add(duplicateSectionB);

                Wall newWall = new Wall(newSections);
                newWall.Thickness = this.Thickness;
                newWall.AssignSections();
                return newWall;
            }
            return null;
        }

        public void AssignSections()
        {
            foreach (WallSection section in _wallSections)
                section.AssignToWall(this);
        }

        public void RemoveEdgeSection(WallSection section)
        {
            if (!WallSectionDeleter.IsSectionOnWallsEdge(section, this)) return;
            List<WallSection> sections = new List<WallSection>(_wallSections);
            sections.Remove(section);
            _wallSections = sections.ToArray();
        }

        public override string ToString()
        {
            string strSections = "Sections: ";

            foreach (WallSection section in _wallSections)
            {
                strSections += section.ToString() + "\n";
            }

            return "Thickness: " + _thickness + "\n" + strSections;
                
        }

        public bool CheckIntegrity()
        {
            foreach(Vector2 point in Points2D)
            {

            }
            return true;
        }

        internal void PutNewSectionAtPosition(WallSection sectionClone, int orderInWall)
        {
            sectionClone.AssignToWall(this);
            List<WallSection> newSections = new List<WallSection>();
            int index = 0;
            foreach(WallSection section in _wallSections)
            {
                if(index == orderInWall)
                    newSections.Add(sectionClone);
                newSections.Add(section);
                index++;
            }
            WallSections = newSections.ToArray();
            PutSectionsInOrder();
        }
    }

   

    public enum WallType
    {
        LoadBearing,
        Partition
    }
}