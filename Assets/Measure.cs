using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class Measure : MonoBehaviour
{
    [SerializeField] GameObject _measurePrefab;
    [SerializeField] float _offset = 50f;
    Storey storey; 

    [ContextMenu("Show Storey Measure")]
    public void ShowStoreyMeasure()
    {
        ClearMeasures();
        storey = GameManager.ins.Building.CurrentStorey;
        foreach (Wall wall in storey.Walls)
        {
            foreach(WallSection section in wall.WallSections)
            {
                GameObject measureInstance = Instantiate(_measurePrefab, transform);
                SingleMeasure measure = measureInstance.GetComponent<SingleMeasure>();
                measure.Draw(section, _offset);
            }
        }
    }

    void ClearMeasures()
    {
        foreach (SingleMeasure measure in gameObject.GetComponentsInChildren<SingleMeasure>())
            Destroy(measure.gameObject);
    }
}
