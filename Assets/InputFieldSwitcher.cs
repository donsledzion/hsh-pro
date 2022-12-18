using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldSwitcher : MonoBehaviour
{
    [SerializeField] TMP_InputField[] _inputs;

    int _selectedIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SelectNext();
    }

    void SelectNext()
    {
        if(_inputs.Length < 1) return;
        _selectedIndex++;
        if(_selectedIndex >= _inputs.Length) _selectedIndex = 0;
        _inputs[_selectedIndex].Select();
    }

    private void OnEnable()
    {
        _selectedIndex = 0;
        _inputs[_selectedIndex].Select();
    }
}
