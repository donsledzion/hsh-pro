using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    VideoPlayer _player;

    [SerializeField] VideoAspectRatio _aspectRatio;

    void Start()
    {
        _player = GetComponent<VideoPlayer>();
        
        _player.Play();
    }

    private void Update()
    {
        _player.aspectRatio = _aspectRatio;
    }
}
