using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveController : MonoBehaviour
{
    [SerializeField] Animator _buttonAnimator;
    [SerializeField] Animator _doorAnimator;
    [SerializeField] AudioTrigger _pingAudioTrigger;
    [SerializeField] float _doorOpenDelay = 0.1f;
    [SerializeField] AudioTrigger _doorOpenAudioTrigger;
    [SerializeField] AudioTrigger _doorCloseAudioTrigger;
    bool _isOpen = false;
    bool _isClosed = true;

    [ContextMenu("PushTheButton")]
    public void PushTheButton()
    {
        
        _buttonAnimator.Play("PushTheButton");
        if (!_isClosed) return;        
        StartCoroutine(OpenDoorSoundCor());
        StartCoroutine(OpenDoorCor());
    }

    IEnumerator OpenDoorSoundCor()
    {
        yield return new WaitForSeconds(_doorOpenDelay);    
        _doorOpenAudioTrigger.PlayAudio();
    }

    IEnumerator OpenDoorCor()
    {
        _isOpen = false;
        _doorAnimator.Play("Open");
        _pingAudioTrigger.PlayAudio();
        yield return new WaitForSeconds(1f);
        _isOpen = true;
    }

    [ContextMenu("Close Door")]
    public void CloseDoor()
    {
        if(!_isOpen) return;    
        StartCoroutine(CloseDoorCor());
    }


    IEnumerator CloseDoorCor()
    {
        _isOpen = false;
        _doorAnimator.Play("Close");
        _doorCloseAudioTrigger.PlayAudio();
        yield return new WaitForSeconds(2f);
        _isClosed = true;   
    }
}
