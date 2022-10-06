using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class setSxVol : MonoBehaviour
{
    public AudioMixer SfxMixer;
    public Slider sfxSlider;

    public void SetLevelOfSfxVolume(float SliderValue)
    {

        SfxMixer.SetFloat("SfxVol", Mathf.Log10(SliderValue) * 20);


    }
    private void Awake()
    {

        float valueofmusic;
        float calculate;
        SfxMixer.GetFloat("SfxVol", out valueofmusic);
        Debug.Log("Sfx volume : " + valueofmusic);
        calculate = Mathf.Pow(10, (valueofmusic / 20));
        Debug.Log("db to float Sfx :" + calculate);
        sfxSlider.value = calculate;
    }
}