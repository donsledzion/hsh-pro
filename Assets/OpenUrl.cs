using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    [SerializeField] string _urlToOpen;
    public void OpenURL(string urlToOpen)
    {
        Application.OpenURL(urlToOpen);
    }
}