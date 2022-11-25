using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window2DPrefab : MonoBehaviour
{
    [SerializeField] Transform _goodView;
    [SerializeField] Transform _badView;
    bool _isGood = true;

    public bool IsGood => _isGood;

    public void BeGood()
    {
        _goodView.gameObject.SetActive(true);
        _badView.gameObject.SetActive(false);
        _isGood = true;
    }
    public void BeBad()
    {
        _goodView.gameObject.SetActive(false);
        _badView.gameObject.SetActive(true);
        _isGood = false;
    }
}
