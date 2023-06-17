using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public RawImage mScreen;
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (mScreen != null && videoPlayer != null)
        {
            StartCoroutine(PrepareVideo());
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    protected IEnumerator PrepareVideo()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        mScreen.texture = videoPlayer.texture;
    }

    public void PlayVideo()
    {
        if (videoPlayer != null && videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if (videoPlayer != null && videoPlayer.isPrepared)
        {
            videoPlayer.Stop();
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    public void OnVideoEnd(VideoPlayer vp)
    {
        if (vp == videoPlayer)
        {
            GameManager.instance.state = GameManager.StateMode.GameOver;
        }
    }
}
