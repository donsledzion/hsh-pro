using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartDelayedEvent : OnStartEvent
{
    [SerializeField] float delayTime = .5f;
    private void Start()
    {
        StartCoroutine("DelayCoroutine");
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        _onStart?.Invoke();
    }
}
