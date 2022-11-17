using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawingParametersController : MonoBehaviour
{
    public static DrawingParametersController ins { get; private set; }

    [SerializeField] TMP_InputField _windowWidth;
    [SerializeField] TMP_InputField _doorWidth;
    

    public float thicknesFactor = 1f;

    public float WindowWidth => float.Parse(_windowWidth.text);
    public float DoorWidth => float.Parse(_doorWidth.text);

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            thicknesFactor += 0.1f;
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            thicknesFactor -= 0.1f;
    }
}
