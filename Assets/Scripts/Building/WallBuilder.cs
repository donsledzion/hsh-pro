using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;
using Walls2D;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine.UI;
//using System.Linq;

public class WallBuilder : MonoBehaviour
{

    [SerializeField] GameObject pointLabelPrefab;
    [SerializeField] public Transform labelsContainer;

    [SerializeField] UILineRenderer _uILineRenderer;
    [SerializeField] UILineRenderer _liveUILineRenderer;
    [SerializeField] RectTransform _whiteboardBackground;
    List<Vector2> _pointList = new List<Vector2>();
    bool _relativeAngle = true;
    bool tmpLabelSpawned = false;
    GameObject tmpLabel;
    [SerializeField] CanvasController _canvasController;
    [SerializeField] TextMeshProUGUI _gridSnapButtonLabel;
    [SerializeField] Slider gridLabelSlider;
    private bool _gridSnap = false;

    List<WallSection> _wallSections = new List<WallSection>();


    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-_whiteboardBackground.rect.width/2, -_whiteboardBackground.rect.height/2, 0);
    }

    void Update()
    {
        Vector3 pointerPosition = Input.mousePosition;
        if(_gridSnap)
            GridSnap(pointerPosition);

        if (Input.GetMouseButtonDown(0) && GameManager.ins.pointerOverUI)
        {
            SpawnPointLabel(pointerPosition, true);
            AddWallSection();
        }

        if (Input.GetKeyDown(KeyCode.A))
            _relativeAngle = !_relativeAngle;
        
        if(GameManager.ins.pointerOverUI)        
            DrawLive(pointerPosition);
        
        if (Input.GetKeyDown(KeyCode.S))
            ToggleGridSnap();
    }

    void AddWallSection()
    {
        Vector2[] allPoints = _uILineRenderer.Points;
        int allPointsCount = allPoints.Length;
        if (allPointsCount < 2) return;
        Vector2[] sectionPoints = { allPoints[allPointsCount - 2], allPoints[allPointsCount - 1] };
        WallSection section = new SectionStraight(sectionPoints);

        _wallSections.Add(section);
    }

    public void ToggleGridSnap()
    {
        _gridSnap = !_gridSnap;
        if (_gridSnap)
            _gridSnapButtonLabel.text = "Grid snap: ON";
        else
            _gridSnapButtonLabel.text = "Grid snap: OFF";
    }

    public void GridSnap(Vector3 pointerPosition)
    {
        float x = GameManager.ins.drawingCanvasBackgroundLBCorner.x
                + Mathf.Round((pointerPosition.x - GameManager.ins.drawingCanvasBackgroundLBCorner.x)
                / (gridLabelSlider.value * GameManager.ins.zoom))
                * (gridLabelSlider.value * GameManager.ins.zoom);

        float y = GameManager.ins.drawingCanvasBackgroundLBCorner.y
            + Mathf.Round((pointerPosition.y - GameManager.ins.drawingCanvasBackgroundLBCorner.y)
            / (gridLabelSlider.value * GameManager.ins.zoom))
            * (gridLabelSlider.value * GameManager.ins.zoom);

        pointerPosition = new Vector3(x, y, 0);
    }

    public void SpawnPointLabel(Vector3 position, bool localPosition = false)
    {
        GameObject label = Instantiate(pointLabelPrefab, position, pointLabelPrefab.transform.rotation);

        GameObject tempScaler = new GameObject("TempScaler");
        label.transform.SetParent(tempScaler.transform);
        tempScaler.transform.localScale = Vector3.one * GameManager.ins.zoom;
        label.transform.SetParent(transform.root);
        label.transform.position = position;
        label.transform.SetParent(labelsContainer);



        Destroy(tempScaler);

        if (localPosition == true)
        {
            Vector3 ovcPos = (position - GameManager.ins.drawingCanvasBackgroundLBCorner) / GameManager.ins.zoom;
            _uILineRenderer.AddPoint(ovcPos);
            _uILineRenderer.LineThickness += 0.1f;
            _uILineRenderer.LineThickness -= 0.1f;

            label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10 * ovcPos.x)) / 10f + " , " + ((int)(10 * ovcPos.y)) / 10f + " ]");
        }
    }

    void DrawLive(Vector3 targetPos)
    {
        int pointsCount = _uILineRenderer.Points.Length;
        if (pointsCount < 1 || (_uILineRenderer.Points[0].x == 0f && _liveUILineRenderer.Points[0].y == 0f)) return;

        Vector2 lastPoint = _uILineRenderer.Points[pointsCount - 1];
        Vector2 pointerPos = (targetPos - GameManager.ins.drawingCanvasBackgroundLBCorner) / GameManager.ins.zoom;
        Vector2 lastVector = Vector2.right;
        if (pointsCount > 1 && _relativeAngle)
            lastVector = _uILineRenderer.Points[pointsCount - 1] - _uILineRenderer.Points[pointsCount - 2];
        Vector2 currentVector = pointerPos - _uILineRenderer.Points[pointsCount - 1];

        float angle = Vector2.Angle(currentVector, lastVector);

        _liveUILineRenderer.Points = new Vector2[] { lastPoint, pointerPos };
            
        if(tmpLabel==null) tmpLabel = Instantiate(pointLabelPrefab, new Vector3(pointerPos.x, pointerPos.y, 0), pointLabelPrefab.transform.rotation);
        tmpLabel.transform.SetParent(labelsContainer);
        tmpLabel.GetComponent<PointLabel>().SetLabelText("" + Mathf.Round((pointerPos - lastPoint).magnitude) + " [cm] | " + Mathf.Round(angle*10)/10f);
        tmpLabel.transform.position = targetPos;

        _liveUILineRenderer.LineThickness += 0.1f;
        _liveUILineRenderer.LineThickness -= 0.1f;
    }

    public void SaveToXml(string fileName)
    {
        SerializeToXML(fileName, _pointList, WallType.LoadBearing);
    }

    public void SaveFromLinqToXml(string fileName)
    {
        createXmlFromLinq(fileName, _wallSections, WallType.LoadBearing);
    }
    void SerializeToXML(string fileName, List<Vector2> points,  WallType wallType)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";

        Stream writer = new FileStream(path, FileMode.Create);

        Wall wall = new Wall(_wallSections);

        XmlSerializer wallSerializer = new XmlSerializer(typeof(Wall));

        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //XmlSerializer storeySerializer = new XmlSerializer(typeof(Storey));
        ns.Add("", "");
        wallSerializer.Serialize(writer, wall, ns);
    }

    void ClearWalls()
    {        
        _uILineRenderer.Points = new Vector2[0];
        _liveUILineRenderer.Points = new Vector2[0];
        _canvasController.ClearPointsLabels();
    }

    public void DeserializeXML(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";

        Wall wall = null;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Wall));
        if(File.Exists(path))
        {
            ClearWalls();
            TextReader textReader = new StreamReader(path);
            wall = (Wall)xmlSerializer.Deserialize(textReader);
            Debug.Log(wall.ToString());
            _wallSections = new List<WallSection>(wall.WallSections);
            Vector2[] _loadedPoints = new Vector2[_wallSections.Count+1];
            for(int i = 0; i < _wallSections.Count; i++)
            {
                _loadedPoints[i] = wall.WallSections[i].StartPoint.Position;
            }
            _loadedPoints[_wallSections.Count] = wall.WallSections[_wallSections.Count - 1].EndPoint.Position;
            _uILineRenderer.Points = _loadedPoints;
            _uILineRenderer.LineThickness += 0.1f;
            _uILineRenderer.LineThickness -= 0.1f;

        }

    }

    private static void createXmlFromLinq(string fileName,List<WallSection> sections, WallType wallType = WallType.LoadBearing)
    {
        Wall wall = new Wall(sections);

        IEnumerable<XElement> linQSections =   from section in sections
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
}
