using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlotButton : ProjectSlotButton
{
    protected override void Start()
    {
        LoadingProjectController loadingController = GetComponentInParent<LoadingProjectController>();
        GetComponent<Button>().onClick.AddListener(() => loadingController.SelectMe(this.gameObject));
        slotText = GetComponentInChildren<TextMeshProUGUI>();
        base.Start();
    }
}
