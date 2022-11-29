using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Valve.VR;

namespace HandMenu
{
    public class LeftHandMenuController : MonoBehaviour
    {
        [SerializeField] GameObject _storeDeleteMenu;
        [SerializeField] GameObject _equipment;

        [SerializeField] SteamVR_Action_Boolean showLeftHandMenu;
        [SerializeField] SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.LeftHand;
        [SerializeField] VRPlayerController _vrPlayerController;
        internal VRPlayerController VRPlayerController => _vrPlayerController;
        private void Start()
        {
            if(_vrPlayerController == null)
                _vrPlayerController = GetComponentInParent<VRPlayerController>();
        }
        private void Update()
        {
            if(showLeftHandMenu != null && showLeftHandMenu.GetStateDown(inputSource))
            {
                _storeDeleteMenu.SetActive(!_storeDeleteMenu.activeSelf);
                _vrPlayerController.enabled = !_storeDeleteMenu.activeSelf;
            }
        }
    }
}
