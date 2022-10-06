using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer VolMixer;
    public Slider musicSlider;


    public void SetLevelOfVolume(float SliderValue){

        VolMixer.SetFloat("MusicVol",Mathf.Log10(SliderValue)*20);

    }

    private void Awake(){

        float valueofmusic;
        float calculate;
        VolMixer.GetFloat("MusicVol", out valueofmusic);
        Debug.Log("Music volume : " + valueofmusic);
        calculate = Mathf.Pow(10,(valueofmusic / 20));
        Debug.Log("db to float :" + calculate);
        musicSlider.value = calculate;
    }
}
