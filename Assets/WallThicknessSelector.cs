using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls2D;
using TMPro;
using System;
using UnityEngine.UI;

public class WallThicknessSelector : MonoBehaviour
{
    [SerializeField] List<WallThicknessTemplate> _wallSizes = new List<WallThicknessTemplate>();
    [SerializeField] TMPro.TMP_Dropdown _dropdown;

    public List<WallThicknessTemplate> WallSizes => _wallSizes;

    private void Start()
    {
        if(_dropdown == null)
            _dropdown = GetComponent<TMP_Dropdown>();
        if (_dropdown == null) return;
        SeedDropdown();
    }

    void SeedDropdown()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        if (_wallSizes.Count <= 0) return;
        {
            foreach (WallThicknessTemplate template in _wallSizes)
                options.Add(new TMP_Dropdown.OptionData(template.Thickness + " - " + template.Name));
        }
        _dropdown.AddOptions(options);
    }

    public void SetCurrentWallThickness()
    {
        int index = gameObject.GetComponent<TMP_Dropdown>().value;
        Drawing2DController.ins.CurrentWallThickness = _wallSizes[index].Thickness;
    }

}