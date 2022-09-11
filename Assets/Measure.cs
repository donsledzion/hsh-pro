using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;

public class Measure : MonoBehaviour
{
    [SerializeField] GameObject _measurePrefab;
    [SerializeField] float _offset = 50f;
    Storey storey;

    bool _isVisible = false;

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
        _isVisible = true;
    }

    public void ClearMeasures()
    {
        foreach (SingleMeasure measure in gameObject.GetComponentsInChildren<SingleMeasure>())
            Destroy(measure.gameObject);
        _isVisible = false;
    }

    public void Toggle()
    {
        if (_isVisible)
            ClearMeasures();
        else
            ShowStoreyMeasure();
    }
}
