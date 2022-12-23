using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PointLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI labelText;

    public void SetText(string text)
    {
        labelText.text = text;
    }
    public void SetText(Vector2 position)
    {
        labelText.text = "[ " + position.x + " , " + position.y + " ]";
    }
    public void SetColorText(Color color)
    {
        labelText.color= color;
    }

    public void SetPosition(Vector3 position, bool local=true)
    {
        if (local)
            transform.localPosition = position;
        else
            transform.position = position;
    }

    public void Setup(Vector2 position, Color color)
    {
        SetText(position);
        SetPosition(position);
        SetColorText(color);
    }
}
