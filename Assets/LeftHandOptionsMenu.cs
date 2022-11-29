using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace HandMenu
{
    public class LeftHandOptionsMenu : MonoBehaviour
    {
        [SerializeField] HandMenuOption[] _handMenuOptions;
        [SerializeField] HandMenuOption _selectedOption;
        UnityEvent _currentActionEvent;
        [SerializeField] string _handToUse = "LeftHand";
        [SerializeField] SteamVR_Action_Vector2 input;
        int _currentOptionIndex;

        private void Update()
        {
            float joystickSwing = input.axis.x;
            if (joystickSwing < -.35f) SelectPreviousOption(false);
            else if (joystickSwing > .35f) SelectNextOption(false);

        }

        private void SelectNextOption(bool allowLooping)
        {
            _currentOptionIndex++;
            if (_currentOptionIndex >= _handMenuOptions.Length)
                _currentOptionIndex = allowLooping ? 0 : _handMenuOptions.Length - 1;
            SelectOption(_handMenuOptions[_currentOptionIndex]);
        }

        private void SelectPreviousOption(bool allowLooping)
        {
            _currentOptionIndex--;
            if (_currentOptionIndex < 0)
                _currentOptionIndex = allowLooping ? _handMenuOptions.Length - 1 : 0;
            SelectOption(_handMenuOptions[_currentOptionIndex]);
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

        [ContextMenu("Fire Event")]
        public void FireEvent()
        {
            foreach(Hand hand in Player.instance.hands)
            {
                Debug.Log(hand.name);
            }
        }
    }


    public enum OptionType
    {
        Delete,
        Store,
        Pick
    }
}
