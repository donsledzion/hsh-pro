using UnityEngine;
using TMPro;

public class FloorListButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _buttonTMP;
    FloorSection2D _floorSection;
    public FloorSection2D FloorSection => _floorSection;
    public void Setup(FloorSection2D section)
    {
        _floorSection = section;
        _buttonTMP.text = section.Order + " - Pod³oga " + section.Area.ToString("0.00" + " m\xB2");
        gameObject.SetActive(true);
    }
}
