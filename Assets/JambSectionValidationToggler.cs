using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambSectionValidationToggler : MonoBehaviour
{
    [SerializeField] List<Transform> _goodView = new List<Transform>();
    [SerializeField] List<Transform> _badView = new List<Transform>();
    bool _isGood = true;

    public bool IsGood => _isGood;

    public void BeGood()
    {
        SetList(_goodView, true);
        SetList(_badView, false);        
        _isGood = true;
    }
    public void BeBad()
    {
        SetList(_goodView, false);
        SetList(_badView, true);
        _isGood = false;
    }

    void SetList(List<Transform> list, bool state)
    {
        foreach (Transform t in list)
            t.gameObject.SetActive(state);
    }
}
