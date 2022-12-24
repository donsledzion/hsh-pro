using UnityEngine;
using TMPro;

public class WallInfoDisplay : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI _wallType;   
    [SerializeField] TextMeshProUGUI _wallThickness;
    [SerializeField] TMP_InputField _wallThicknessInput;
    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] WallThicknessSelector _thicknessSelector;

    public static float LastThickness;
    private void Start()
    {
        if(_dropdown == null)
            _dropdown = GetComponent<TMP_Dropdown>();

    }


    [ContextMenu("UpdateInfo")]
    public void UpdateInfo()
    {
        //wallType.text = Drawing2DController.ins.
    }

    public void UpdateWallThicknessInputField()
    {
        _wallThicknessInput.text = _thicknessSelector.WallSizes[_dropdown.value].Thickness.ToString();
    }
    public void UpdateCurrentWallThickness()
    {
        Drawing2DController.ins.CurrentWallThickness = float.Parse(_wallThicknessInput.text);
        LastThickness = Drawing2DController.ins.CurrentWallThickness;
    }
}
