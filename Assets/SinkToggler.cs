using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkToggler : MonoBehaviour
{
    [SerializeField] ParticleSystem _waterParticles;
    [SerializeField] AudioTrigger _waterStart;
    [SerializeField] AudioTrigger _waterStop;
    [SerializeField] AudioTrigger _waterLoop;
    bool _isRunning = false;

    public void Toggle()
    {
        if (_isRunning)
            StopWater();
        else
            StartWater();
    }

    IEnumerator StartWaterCor()
    {
        _waterParticles.gameObject.SetActive(true);
        _waterStart.PlayAudio();
        yield return new WaitForSeconds(1.4f);
        _waterLoop.PlayAudio();
    }

    [ContextMenu("Start Water")]
    void StartWater()
    {
        _isRunning = true;
        StartCoroutine(StartWaterCor());
    }

    [ContextMenu("Stop Water")]
    void StopWater()
    {
        _isRunning = false;
        StopAllCoroutines();
        _waterLoop.AudioSource.Stop();
        _waterStop.PlayAudio();
        _waterParticles.gameObject.SetActive(false);
    }
}
