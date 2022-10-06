using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chageResolution(Dropdown sender) {

        Debug.Log("value of DD" + sender.value);

        if (sender.value == 0)
        {

            Screen.SetResolution(1920, 1080, true);

        }

        if (sender.value == 1)
        {

            Screen.SetResolution(1366, 768, true);

        }

        if (sender.value == 2)
        {

            Screen.SetResolution(2560, 1440, true);

        }

        if (sender.value == 3)
        {

            Screen.SetResolution(3840, 2160, true);

        }

    }
}
