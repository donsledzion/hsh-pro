using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggler : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnEnable()
    {
        gameManager.CurrentCamera = gameObject.GetComponent<Camera>();
    }
}
