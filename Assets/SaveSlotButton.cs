using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotButton : ProjectSlotButton
{
    protected override void Start()
    {
        SavingProjectController savingController = GetComponentInParent<SavingProjectController>();
        GetComponent<Button>().onClick.AddListener(() => savingController.SelectMe(this.gameObject));
        slotText = GetComponentInChildren<TextMeshProUGUI>();
        base.Start();
    }
}
