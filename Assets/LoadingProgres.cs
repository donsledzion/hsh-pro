using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgres : MonoBehaviour
{
    [SerializeField] float fillSpeed = 2f;
    [SerializeField] Color fillColor;
    Image image;
    bool filling = true;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(image.fillAmount >= 1) filling = false;
        
        if (filling)
        {
            image.fillClockwise = true;
            image.fillAmount += fillSpeed * Time.deltaTime;
        }
        else
        {
            image.fillClockwise = false;
            image.fillAmount -= fillSpeed * Time.deltaTime;
        }        
        if(image.fillAmount<=0) filling = true;
    }
}
