using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Valve.VR;

public class HiFiToggler : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    int currentStation = 0;
    [SerializeField] bool isUp = false;
    [SerializeField] UnityEvent onStateUp;
    [SerializeField] UnityEvent onStateDown;

    [SerializeField] SteamVR_Action_Boolean _toggleDevice;
    [SerializeField] public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    [SerializeField] public SteamVR_Action_Boolean _swapStation;

    public bool IsHovered { get; set; } = false;

    private void Start()
    {
        LoadNextSong();
    }

    private void Update()
    {
        if (VRPlayerStatusReport.ins.IsActive)
        {
            if (_toggleDevice != null && _toggleDevice.GetStateDown(inputSource) && IsHovered)
                Toggle();
            if (_swapStation != null && _swapStation.GetStateDown(inputSource) && IsHovered)
                SwapStation();
        }
    }

    public void Toggle()
    {
        if (isUp)
        {
            onStateDown?.Invoke();
            isUp = false;
        }
        else
        {
            onStateUp?.Invoke();
            isUp = true;
        }
    }

    void LoadNextSong()
    {
        _audioSource.clip = MusicController.ins.NextSong();
    }

    public void SwapStation()
    {
        _audioSource.Stop();
        LoadNextSong();
        _audioSource.Play();
    }
}
