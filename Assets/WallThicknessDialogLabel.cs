using UnityEngine;
using TMPro;

public class WallThicknessDialogLabel : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;

    private void Start()
    {
        _inputField.text = ValidateInput(Drawing2DController.ins.CurrentWallThickness).ToString();
        _inputField.Select();
    }

    public float ValidateInput(string inputText)
    {
        if (string.IsNullOrEmpty(inputText))
            inputText = "5";
        float inputFloat = float.Parse(inputText);
        return ValidateInput(inputFloat);
    }
    public float ValidateInput(float inputFloat)
    {
        if (inputFloat < 5f) return 5f;
        return inputFloat;
    }
    public float ValidateInput()
    {
        return ValidateInput(_inputField.text);
    }
    public void SelfValidate()
    {
        _inputField.text = ValidateInput(_inputField.text).ToString();
    }
}
