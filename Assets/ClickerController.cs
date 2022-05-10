using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerController : MonoBehaviour
{
    [SerializeField] GameObject pointLabelPrefab;
    [SerializeField] public Transform labelsContainer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.ins.pointerOverUI)
        {            
            SpawnPointLabel(Input.mousePosition, true);
        }
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

        if (localPosition==true)
        {
            Vector3 ovcPos = (position - GameManager.ins.drawingCanvasBackgroundLBCorner)/GameManager.ins.zoom;

            label.GetComponent<PointLabel>().SetLabelText("[ " + ((int)(10*ovcPos.x))/10f + " , " + ((int)(10*ovcPos.y))/10f + " ]");
        }
    }

}
