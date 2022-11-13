using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SunlightConfigSpotToggler : MonoBehaviour
{
    public SteamVR_Action_Boolean showSunConfigMenu;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.RightHand;

    [SerializeField] Transform _configSpotTransform;

    private void Update()
    {
        if (showSunConfigMenu != null && showSunConfigMenu.GetStateDown(inputSource))
            _configSpotTransform.gameObject.SetActive(!_configSpotTransform.gameObject.activeSelf);
    }
}
