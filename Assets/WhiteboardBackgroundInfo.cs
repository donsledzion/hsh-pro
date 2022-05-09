using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhiteboardBackgroundInfo : MonoBehaviour
{
    [SerializeField] Transform whiteboardHolder;
    [SerializeField] TextMeshProUGUI LBCornerLabel;
    [SerializeField] GameManager gameManager;


    void Update()
    {
        float zoom = gameManager.zoom;
        Vector3 whiteboardPos = whiteboardHolder.position;

        RectTransform myRect = GetComponent<RectTransform>();

        Vector3 lbCornerPos = new Vector3(
            whiteboardHolder.position.x - myRect.rect.width*zoom/2
            ,
            whiteboardHolder.position.y - myRect.rect.height* zoom / 2
            , 0
            );

        LBCornerLabel.text = "LBCorner at: [" + lbCornerPos.x + " , " + lbCornerPos.y + "]";

        gameManager.drawingCanvasBackgroundLBCorner = lbCornerPos;
    }
}
