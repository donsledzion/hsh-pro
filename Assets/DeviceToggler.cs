using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Valve.VR;

public class DeviceToggler : MonoBehaviour
{
    [SerializeField] VideoClip[] _clips;
    [SerializeField] VideoPlayer _player;
    int currentStation = 0;
    [SerializeField] bool isUp = false;
    [SerializeField] UnityEvent onStateUp;
    [SerializeField] UnityEvent onStateDown;

    [SerializeField] SteamVR_Action_Boolean _toggleDevice;
    [SerializeField] public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    [SerializeField] public SteamVR_Action_Boolean _swapStation;

    public bool IsHovered { get; set; } = false;

    private void Update()
    {
        if (_toggleDevice != null && _toggleDevice.GetStateDown(inputSource) && IsHovered)
            Toggle();
        if (_swapStation != null && _swapStation.GetStateDown(inputSource) && IsHovered)
            SwapStation();
    }

    public void Toggle()
    {
        if(isUp)
        {
            onStateDown?.Invoke();
            isUp = false;
        }
        else
        {
            onStateUp?.Invoke();
            isUp=true;
        }
    }

    public void SwapStation()
    {
        if (_clips.Length <= 1) return;
        currentStation++;
        if(currentStation >= _clips.Length)
            currentStation = 0;
        _player.Stop();
        _player.clip = _clips[currentStation];
        _player.Play();
    }
}
