using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HandMenu
{
    public class HandMenuOption : MonoBehaviour
    {
        [SerializeField] OptionType optionType;
        [SerializeField] Outline outline;
        [SerializeField] UnityEvent _eventToFire;
        [SerializeField] AudioSource _optionConfirmAudioSource;
        public UnityEvent EventToFire => _eventToFire;


        internal void Select(bool state)
        {
            outline.enabled = state;
        }
    }
}
