using UnityEngine;
using TMPro;
using Walls2D;

public class WallInfoDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _wallType;   
    [SerializeField] TextMeshProUGUI _wallWidth;

    [ContextMenu("UpdateInfo")]
    public void UpdateInfo()
    {
        //wallType.text = Drawing2DController.ins.
    }

    public void UpdateWallType(string typeName)
    {
        WallType wallType = 
        Drawing2DController.ins.CurrentWallType;
        //    _wallType.text = "Type: " + wallType.ToString();
       //_wallType.text = "Wybrano: " + typeName;
    }

    public void UpdateWallWidth()
    {

    }
}
