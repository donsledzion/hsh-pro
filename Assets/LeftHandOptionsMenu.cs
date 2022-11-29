using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Hand = Valve.VR.InteractionSystem.Hand;

namespace HandMenu
{
    public class LeftHandOptionsMenu : MonoBehaviour
    {
        [SerializeField] HandMenuOption[] _handMenuOptions;
        [SerializeField] HandMenuOption _selectedOption;
        UnityEvent _currentActionEvent;
        [SerializeField] string _handToUse = "LeftHand";
        [SerializeField] SteamVR_Action_Vector2 navigate;
        [SerializeField] SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.LeftHand;
        [SerializeField] SteamVR_Action_Boolean fireEvent;
        int _currentOptionIndex;
        GameObject _currentlyHeldObject;
        [SerializeField] AudioSource _changeOptionAudioSource;
        bool _changeOptionCooldown = false;
        [SerializeField] LeftHandMenuController _leftHandMenuController;
        private void Update()
        {
            float joystickSwing = navigate.axis.x;
            if (joystickSwing < -.35f) SelectPreviousOption(false);
            else if (joystickSwing > .35f) SelectNextOption(false);
            if (fireEvent != null && fireEvent.GetStateDown(inputSource))
                FireEvent();

        }

        private void SelectNextOption(bool allowLooping)
        {
            if(!_changeOptionCooldown)
            {
                _currentOptionIndex++;
                if (_currentOptionIndex >= _handMenuOptions.Length)
                    _currentOptionIndex = allowLooping ? 0 : _handMenuOptions.Length - 1;
                SelectOption(_handMenuOptions[_currentOptionIndex]);
                StartCoroutine(ChangeOptionCooldownCor());
            }
        }

        private void SelectPreviousOption(bool allowLooping)
        {
            if(!_changeOptionCooldown)
            {
                _currentOptionIndex--;
                if (_currentOptionIndex < 0)
                    _currentOptionIndex = allowLooping ? _handMenuOptions.Length - 1 : 0;
                SelectOption(_handMenuOptions[_currentOptionIndex]);
                StartCoroutine(ChangeOptionCooldownCor());

            }
        }

        private void OnEnable()
        {
            if (_handMenuOptions != null && _handMenuOptions.Length > 0)
            {
                _currentOptionIndex = 0;   
                SelectOption(_handMenuOptions[_currentOptionIndex]);
            }
        }

        void SelectOption(HandMenuOption selectedOption)
        {
            _changeOptionAudioSource.Play();
            foreach (HandMenuOption option in _handMenuOptions)
            {
                if (option != selectedOption) option.Select(false);
                else
                {
                    option.Select(true);
                    _selectedOption = option;
                    _currentActionEvent = option.EventToFire;
                }
            }
        }

        IEnumerator ChangeOptionCooldownCor()
        {
            _changeOptionCooldown = true;
            yield return new WaitForSeconds(.5f);
            _changeOptionCooldown = false;
        }

        [ContextMenu("Fire Event")]
        public void FireEvent()
        {
            foreach(Hand hand in Player.instance.hands)
            {
                if(hand.name == _handToUse)
                {
                    _currentlyHeldObject = hand.currentAttachedObject;
                    if(_currentlyHeldObject != null)
                    {
                        hand.DetachObject(_currentlyHeldObject);
                        _currentActionEvent.Invoke();
                    }
                    Debug.Log(hand.currentAttachedObjectInfo);
                }
            }
        }

        public void DeleteItem()
        {
            
            Destroy(_currentlyHeldObject);
            _currentlyHeldObject = null;
            _leftHandMenuController.VRPlayerController.enabled = true;
            gameObject.SetActive(false);
        }

        public void StoreInInventory()
        {
            _currentlyHeldObject = null;
            _leftHandMenuController.VRPlayerController.enabled = true;
            gameObject.SetActive(false);
        }
    }


    public enum OptionType
    {
        Delete,
        Store,
        Pick
    }
}
