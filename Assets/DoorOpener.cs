using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string _openState = "jj_door_white_open";
    [SerializeField] string _closeState = "jj_door_white_close";
    [SerializeField] bool _isOpen = false;
    [SerializeField] bool _isClosed = true;
    [SerializeField] bool _isToggling = false;


    [ContextMenu("Toggle")]
    public void Toggle()
    {
        if (_isToggling) return;
        if (_isOpen) StartCoroutine("CloseDoor");
        else if (_isClosed) StartCoroutine("OpenDoor");
        else Debug.LogWarning("Unknown door state!");
    }

    //[ContextMenu("Open Door")]
    public IEnumerator OpenDoor()
    {
        if(!_isToggling)
        {
            _isToggling = true;
            animator.enabled = true;                                                        
            animator.Play(_openState);
        }
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        _isClosed = false;
        _isOpen = true;
        _isToggling = false;
    }

    //[ContextMenu("Close Door")]
    public IEnumerator CloseDoor()
    {
        if (!_isToggling)
        {
            _isToggling = true;
            animator.enabled = true;
            animator.Play(_closeState);
        }
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        _isOpen = false;
        _isClosed = true;
        _isToggling = false;
    }
}
