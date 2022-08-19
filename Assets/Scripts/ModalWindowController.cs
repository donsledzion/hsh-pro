using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowController : MonoBehaviour
{

    [SerializeField]
    private GameObject modalWindow;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Button createButton;

    public void Show()
    {
        modalWindow.SetActive(true);
    }

    public void Hide()
    {

        modalWindow.SetActive(false);

    }

}
