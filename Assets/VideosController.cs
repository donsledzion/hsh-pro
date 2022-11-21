using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideosController : MonoBehaviour
{
    public static VideosController ins { get; private set; }

    List<VideoClip> _videoClips = new List<VideoClip>();

    public List<VideoClip> VideoClips { get { return _videoClips; } }

    int _currentVideo = 0;

    public VideoClip NextVideo()
    {
        if (_videoClips.Count < 1) return null;
        _currentVideo++;
        if (_currentVideo > _videoClips.Count-1) _currentVideo = 0;
        return _videoClips[_currentVideo];
    }


    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }


}
