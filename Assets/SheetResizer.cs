using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SheetResizer : MonoBehaviour
{
    WhiteboardBackgroundInfo _sheet => ReferenceController.ins.WhiteboardBackgroundInfo;
    [SerializeField] TMP_InputField _newWidth;
    [SerializeField] TMP_InputField _newHeight;
    [SerializeField] TMP_InputField _sheetName;
    [SerializeField] TextMeshProUGUI _validationErrorDisplay;
    [SerializeField] float minOffset = 50f;
    StoreyPointsCollector _storeyPointsCollector;
    Vector2 _newSize;
    private void Start()
    {
        _storeyPointsCollector = ReferenceController.ins.StoreyPointsCollector;
        _newWidth.text = GameManager.ins.Building.SheetSize.x.ToString();
        _newHeight.text = GameManager.ins.Building.SheetSize.y.ToString();
    }

    private void OnEnable()
    {
        _sheetName.text = GameManager.ins.Building.Name;
    }

    public void ResizeSheet()
    {
        _newSize = new Vector2(int.Parse(_newWidth.text), int.Parse(_newHeight.text));
        if (!ValidateNewSize()) return;
        _sheet.MyRect.sizeDelta= _newSize;
        Drawing2DController.ins.AdjustCanvasPosition();
        CanvasController.ins.ResetCanvas();
        gameObject.SetActive(false);

    }

    private bool ValidateNewSize()
    {
        float minX = 0;
        float minY = 0;
        float maxX = 0;
        float maxY = 0;
        if(GameManager.ins.Building.Points.Length > 2)
        {
            foreach(Storey storey in GameManager.ins.Building.Storeys)
            {
                Vector2[] vect = PolygonHelper.PlaneRange(
                    _storeyPointsCollector.WallPointsListToVectorArray(
                        _storeyPointsCollector.CollectAllPoints(
                            GameManager.ins.Building.Storeys[0])));

                if (vect[0].x < minX) minX = vect[0].x;
                if (vect[0].y < minY) minY = vect[0].y;
                if (vect[1].x > maxX) maxX = vect[1].x;
                if (vect[1].y > maxY) maxY = vect[1].y;
            }

            Vector2[] newOutVectors = { new Vector2(minX, minY), new Vector2(maxX, maxY) };

            Vector2[] boundingRectangle = PolygonHelper.RangeToRectangle(newOutVectors);

            Vector2 minSize = new Vector2(
                boundingRectangle[2].x - boundingRectangle[0].x,
                boundingRectangle[1].y - boundingRectangle[0].y
                );
            if((minSize.x + minOffset) > _newSize.x || (minSize.y + minOffset) > _newSize.y)
            {
                _validationErrorDisplay.text = "Zbyt ma³y rozmiar nowego arkusza. Obecny budynek siê na nim nie zmieœci";
                return false;
            }
        }

        return true;
    }
}
