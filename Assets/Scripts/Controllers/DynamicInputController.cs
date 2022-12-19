using UnityEngine;
using TMPro;

public class DynamicInputController : MonoBehaviour
{
    int _dynamicInputLength = 0;
    string _dynamicInputString = "";
    [SerializeField] GameObject inputLabelPrefab;
    DynamicInputLabel inputLabelInstance;

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

    public static DynamicInputController ins { get; private set; }
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

    public void DisposeOfLabel()
    {
        if (inputLabelInstance == null) return;
        GameObject labelObject = inputLabelInstance.gameObject;
        Destroy(inputLabelInstance);
        Destroy(labelObject);
        inputLabelInstance = null;
    }

    public void ToggleDynamicInput()
    {
        GameManager.ins.ToggleDynamicDimensions();
    }

    public void ResetDynamicInput()
    {
        _dynamicInputLength = 0;
        _dynamicInputString = "";
        if(inputLabelInstance == null)
            inputLabelInstance.SetText(_dynamicInputString);
        Debug.Log("Total: " + _dynamicInputString);
    }

    public void DynamicInput()
    {
        if (!GameManager.ins.DynamicDimensions) return;
        if (inputLabelInstance == null)
        {
            GameObject labelGO = Instantiate(inputLabelPrefab, Drawing2DController.ins.transform);
            inputLabelInstance = labelGO.GetComponent<DynamicInputLabel>();
        }
        if(GameManager.ins.PointerOverUI)
            inputLabelInstance.transform.position = Input.mousePosition;

        if (!GameManager.ins.DynamicDimensions) return;

        int typedInt = TryReadDigit();
        if (typedInt > -1)
        {
            _dynamicInputLength++;
            _dynamicInputString += typedInt;
        }
        inputLabelInstance.SetText(_dynamicInputString);
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
