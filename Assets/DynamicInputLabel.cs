using UnityEngine;
using TMPro;
public class DynamicInputLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _labelText;

    public void SetText(string value)
    {
        _labelText.text = value;
    }

}
