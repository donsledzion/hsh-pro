using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnHover : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    [Range(0f, 1f)]
    [SerializeField] float hoverVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] float clickVolume = 1f;

    public void HoverSound()
    {
        myFx.volume = hoverVolume;
        myFx.PlayOneShot(hoverFX);
    }

    public void ClickSound()
    {
        myFx.volume = clickVolume;
        myFx.PlayOneShot(clickFX);
        
    }
}
