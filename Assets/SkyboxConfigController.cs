using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SkyboxConfigController : MonoBehaviour
{
    public SteamVR_Action_Boolean switchToNextSkybox;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.RightHand;

    [SerializeField] Material[] _skyboxes;

    int currentIndex = 0;

    Material NextSkybox()
    {
        currentIndex++;
        if (currentIndex >= _skyboxes.Length)
            currentIndex = 0;
        return _skyboxes[currentIndex]; 
    }

    Material PreviousSkybox()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = _skyboxes.Length - 1;
        return _skyboxes[currentIndex];
    }

    void SwitchSkybox()
    {
        RenderSettings.skybox = NextSkybox();
    }

    private void Update()
    {
        if (switchToNextSkybox != null && switchToNextSkybox.GetStateDown(inputSource))
            SwitchSkybox();
    }


}
