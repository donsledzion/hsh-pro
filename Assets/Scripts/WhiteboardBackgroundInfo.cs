using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WhiteboardBackgroundInfo : MonoBehaviour
{
    [SerializeField] Transform whiteboardHolder;
    [SerializeField] TextMeshProUGUI LBCornerLabel;

    private Vector3 referenceResolution = new Vector2(1920f, 1080f);

    void Update()
    {
        Vector2 resolution = new Vector2(Screen.width,Screen.height);
        Vector2 resolutionRatio = resolution / referenceResolution;
        Debug.Log("Resolution ratio: " + resolutionRatio);
        float zoom = GameManager.ins.Zoom;
        Vector3 whiteboardPos = whiteboardHolder.position;

        RectTransform myRect = GetComponent<RectTransform>();


        Vector3 lbCornerPos = new Vector3(
            whiteboardHolder.position.x - myRect.rect.width*zoom*resolutionRatio.x/2
            ,
            whiteboardHolder.position.y - myRect.rect.height* zoom*resolutionRatio.y / 2
            , 0
            );

        LBCornerLabel.text = "LBCorner at: [" + lbCornerPos.x + " , " + lbCornerPos.y + "]";

        GameManager.ins.DrawingCanvasBackgroundLBCorner = lbCornerPos;
    }
}
