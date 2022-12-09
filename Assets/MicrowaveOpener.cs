using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveOpener : MonoBehaviour
{
    [SerializeField] Rigidbody _doorRb;
    [SerializeField] float _openingForce = 10f;
    [SerializeField] AudioTrigger _pushButtonAudioTrigger;
    [SerializeField] Animator _buttonAnimator;

    [ContextMenu("PushTheButton")]
    public void PushTheButton()
    {
        _doorRb.AddForce(_openingForce*_doorRb.transform.forward , ForceMode.Impulse);
    }
}
