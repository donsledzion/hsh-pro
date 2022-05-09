using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI zoomIndicator;

    [SerializeField] float maxZoom = 10f;
    [SerializeField] float minZoom = 0.5f;
    [SerializeField] public float outZoom = 1f;
    [SerializeField] float scrollScale = 0.1f;
    [SerializeField] GameManager gameManager;

    float lastZoom = 1f;

    private void Start()
    {
        gameManager.zoom = outZoom;
    }

    void Update()
    {
        outZoom = gameManager.zoom;
        if(Input.mouseScrollDelta.y != 0)
        {
            Debug.Log("Wheel delta: " + Input.mouseScrollDelta.y);
            outZoom = Mathf.Clamp((int)((outZoom + Input.mouseScrollDelta.y * scrollScale)*10f)/10f, minZoom,maxZoom);
        }

        zoomIndicator.text = "Zoom: " + outZoom.ToString();

        if (lastZoom != outZoom)
            gameManager.RedrawCanvas(Input.mousePosition, outZoom, lastZoom);

        lastZoom = outZoom;

        gameManager.zoom = outZoom;
    }

    
}
