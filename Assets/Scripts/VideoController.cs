using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    
    [Tooltip("Time in seconds to jump when seeking")]
    public float seekStep = 5.0f;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    public void TogglePlayPause()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }
        else
        {
            _videoPlayer.Play();
        }
    }

    public void Play()
    {
        if (!_videoPlayer.isPlaying) _videoPlayer.Play();
    }

    public void Pause()
    {
        if (_videoPlayer.isPlaying) _videoPlayer.Pause();
    }

    public void SeekForward()
    {
        _videoPlayer.time += seekStep;
    }

    public void SeekBackward()
    {
        _videoPlayer.time = Mathf.Max(0, (float)_videoPlayer.time - seekStep);
    }

    public void SetTime(float time)
    {
        _videoPlayer.time = Mathf.Clamp(time, 0, (float)_videoPlayer.length);
    }
    
    public double GetCurrentTime() => _videoPlayer.time;
    public double GetDuration() => _videoPlayer.length;
    public bool IsPlaying() => _videoPlayer.isPlaying;
}
