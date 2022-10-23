using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera prefabCamera;
    [SerializeField] private float zoomLerpSpeed = 10;
    float targetZoom;
    float zoomFactor = 3f;
    public void Zoom_In_Out() {

        //targetZoom = prefabCamera.orthographicSize;
    }

    private void Start()
    {
        targetZoom = prefabCamera.orthographicSize;
    }

    private void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 1f, 8.0f);
        prefabCamera.orthographicSize = Mathf.Lerp(prefabCamera.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        
    }

}
