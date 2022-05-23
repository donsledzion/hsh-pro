using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zoomIndicator;
    [SerializeField] CanvasController canvasController;

    [SerializeField] float maxZoom = 10f;
    [SerializeField] float minZoom = 0.5f;
    [SerializeField] public float outZoom = 1f;
    [SerializeField] float scrollScale = 0.1f;

    float lastZoom = 1f;

    private void Start()
    {
        GameManager.ins.Zoom = outZoom;
    }

    void Update()
    {
        outZoom = GameManager.ins.Zoom;
        if(Input.mouseScrollDelta.y != 0)
        {
            //Debug.Log("Wheel delta: " + Input.mouseScrollDelta.y);
            outZoom = Mathf.Clamp(Mathf.Round((outZoom + Input.mouseScrollDelta.y * scrollScale)*10f)/10f, minZoom,maxZoom);
        }

        zoomIndicator.text = "Zoom: " + outZoom.ToString();

        if (lastZoom != outZoom)
            canvasController.RedrawCanvas(Input.mousePosition, outZoom, lastZoom);

        lastZoom = outZoom;

        GameManager.ins.Zoom = outZoom;
    }

    
}
