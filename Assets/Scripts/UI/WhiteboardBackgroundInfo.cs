using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI.Extensions.Tweens;

public class WhiteboardBackgroundInfo : MonoBehaviour
{
    [SerializeField] Transform whiteboardHolder;
    [SerializeField] TextMeshProUGUI LBCornerLabel;
    [Range(0f,2f)]
    [SerializeField] float resolutionMismatchCorrectX = 1;
    [Range(0f, 2f)]
    [SerializeField] float resolutionMismatchCorrectY = 1;

    private Vector3 referenceResolution => new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    RectTransform myRect;

    float zoom => GameManager.ins.Zoom;

    public RectTransform MyRect => myRect;

    private void Start()
    {
        myRect = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector2 resolution = new Vector2(Screen.width,Screen.height);
        Vector2 resolutionRatio = resolution / referenceResolution;
        float screenResolutionRatio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        GameManager.ins.SetResolutionRatio(
            new Vector2(resolutionRatio.x*ResolutionMismatchCorrect(true),
            resolutionRatio.y * ResolutionMismatchCorrect(false))); 

        Vector3 lbCornerPos = new Vector3(
            whiteboardHolder.position.x - myRect.rect.width*zoom*(resolutionRatio.x * ResolutionMismatchCorrect(true))/2
            ,
            whiteboardHolder.position.y - myRect.rect.height* zoom*(resolutionRatio.y * ResolutionMismatchCorrect(false)) / 2
            , 0
            );

        LBCornerLabel.text = "LBCorner at: [" + lbCornerPos.x + " , " + lbCornerPos.y + "]";

        GameManager.ins.SetLBCorner(lbCornerPos);
    }

    public static float ResolutionMismatchCorrect(bool onAxisX)
    {
        float screenResolutionRatio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        float windowResolutionRatio = (float)Screen.width / (float)Screen.height;

        float ratio = windowResolutionRatio / screenResolutionRatio;

        if (onAxisX)
        {
            if (ratio > 1f) return 1f;
            return ratio;
        }
        else
        {
            if (ratio > 1f) return ratio;
            return 1f;
        }
    }
}
