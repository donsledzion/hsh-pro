using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Walls2D;

public class Wall2D : MonoBehaviour
{
    [SerializeField] GameObject sectionStraightPrefab;
    [SerializeField] GameObject sectionArcPrefab;
    [SerializeField] GameObject sectionDoorjambPrefab;
    [SerializeField] GameObject sectionWindowjambPrefab;

    GameObject sectionInstance;

    Wall _wall;



    public void DrawOnCanvas(Wall wall)
    {
        _wall = wall;
        foreach (WallSection section in _wall.WallSections)
        {
            if (section is SectionStraight)
            {
                sectionInstance = Instantiate(sectionStraightPrefab, transform);
            }
            else if (section is SectionArc)
            {
                sectionInstance = Instantiate(sectionArcPrefab, transform);
            }
            else if (section is Doorjamb)
            {
                sectionInstance = Instantiate(sectionDoorjambPrefab, transform);
            }
            else if (section is Windowjamb)
            {
                sectionInstance = Instantiate(sectionWindowjambPrefab, transform);
            }
            section.AssignToWall(wall);
            WallSection2D section2D = sectionInstance.GetComponent<WallSection2D>();            
            section2D.DrawOnCanvas(section);
        }
    }
}
