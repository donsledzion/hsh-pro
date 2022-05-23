using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInputController : MonoBehaviour
{
    int _dynamicInputLength = 0;
    string _dynamicInputString = "";

    public string DynamicInputString
    {
        get { return _dynamicInputString; }
        set { _dynamicInputString = value; }
    }
    public int DynamicInputLength
    {
        get { return _dynamicInputLength; }
        set { _dynamicInputLength = value; }
    }

    public void ToggleDynamicInput()
    {
        GameManager.ins.ToggleDynamicDimensions();
    }

    public void ResetDynamicInput()
    {
        _dynamicInputLength = 0;
        _dynamicInputString = "";
        Debug.Log("Total: " + _dynamicInputString);
    }

    public void DynamicInput()
    {
        if (!GameManager.ins.DynamicDimensions) return;

        int typedInt = TryReadDigit();
        if (typedInt > -1)
        {
            _dynamicInputLength++;
            _dynamicInputString += typedInt;
        }

        Debug.Log("Total: " + _dynamicInputString);
    }

    private int TryReadDigit()
    {
        KeyCode[] keyCodes = {
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
         KeyCode.Keypad0,
         KeyCode.Keypad1,
         KeyCode.Keypad2,
         KeyCode.Keypad3,
         KeyCode.Keypad4,
         KeyCode.Keypad5,
         KeyCode.Keypad6,
         KeyCode.Keypad7,
         KeyCode.Keypad8,
         KeyCode.Keypad9,
     };

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                int numberPressed = i % 10;
                Debug.Log(numberPressed);
                return numberPressed;
            }
        }
        return -1;
    }

}
