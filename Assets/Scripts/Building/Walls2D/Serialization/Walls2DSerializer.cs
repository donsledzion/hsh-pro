using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Walls2D
{
    public class Walls2DSerializer : MonoBehaviour
    {
        public static void SaveFromLinqToXml(string fileName, List<WallSection> sections)
        {
            CreateXmlFromLinq(fileName, sections, WallType.LoadBearing);
        }

        private static void CreateXmlFromLinq(string fileName, List<WallSection> sections, WallType wallType = WallType.LoadBearing)
        {
            Wall wall = new Wall(sections);

            IEnumerable<XElement> linQSections = from section in sections
                                                 select new XElement("section",
                                                         new XElement("start",
                                                             new XAttribute("x", section.StartPoint.Position.x),
                                                             new XAttribute("y", section.StartPoint.Position.y)
                                                         ),
                                                         new XElement("end",
                                                             new XAttribute("x", section.EndPoint.Position.x),
                                                             new XAttribute("y", section.EndPoint.Position.y)
                                                         )
                                                     );

            XElement rootNode = new XElement("wall",
                                        new XAttribute("type", wall.WallType),
                                        new XElement("sections", linQSections)
                                    ); //create a root node to contain the query results
            string path = Application.persistentDataPath + "/" + fileName + ".xml";
            rootNode.Save(path);
        }


        public static void SaveToXml(string fileName, List<Vector2> points, List<WallSection> sections)
        {
            SerializeToXML(fileName, points, WallType.LoadBearing, sections);
        }


        public static void SerializeToXML(string fileName, List<Vector2> points, WallType wallType, List<WallSection> sections)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".xml";

            Stream writer = new FileStream(path, FileMode.Create);

            Wall wall = new Wall(sections);

            XmlSerializer wallSerializer = new XmlSerializer(typeof(Wall));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //XmlSerializer storeySerializer = new XmlSerializer(typeof(Storey));
            ns.Add("", "");
            wallSerializer.Serialize(writer, wall, ns);
        }


        public static void DeserializeXML(string fileName, List<WallSection> sections, UILineRenderer uILineRenderer)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".xml";

            Wall wall = null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Wall));
            if (File.Exists(path))
            {
                // should implement some signal / event here to clear walls is static way //ClearWalls();
                TextReader textReader = new StreamReader(path);
                wall = (Wall)xmlSerializer.Deserialize(textReader);
                Debug.Log(wall.ToString());
                sections = new List<WallSection>(wall.WallSections);
                Vector2[] _loadedPoints = new Vector2[sections.Count + 1];
                for (int i = 0; i < sections.Count; i++)
                {
                    _loadedPoints[i] = wall.WallSections[i].StartPoint.Position;
                }
                _loadedPoints[sections.Count] = wall.WallSections[sections.Count - 1].EndPoint.Position;
                uILineRenderer.Points = _loadedPoints;
                uILineRenderer.LineThickness += 0.1f;
                uILineRenderer.LineThickness -= 0.1f;
            }
        }
    }
}