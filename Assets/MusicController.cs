using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MusicController : MonoBehaviour
{
    public static MusicController ins { get; private set; }

    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>();

    public List<AudioClip> VideoClips { get { return _audioClips; } }

    int _currentSong = 0;

    public AudioClip NextSong()
    {
        if (_audioClips.Count < 1) return null;
        _currentSong++;
        if (_currentSong > _audioClips.Count - 1) _currentSong = 0;
        return _audioClips[_currentSong];
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
