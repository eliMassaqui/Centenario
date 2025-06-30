using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerUI : MonoBehaviour
{
    public GameObject painelVideo;    // arraste o painel aqui
    public VideoPlayer videoPlayer;   // arraste o VideoPlayer aqui

    public void TocarClip(VideoClip clip)
    {
        painelVideo.SetActive(true);
        videoPlayer.Stop();
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void FecharVideo()
    {
        videoPlayer.Stop();
        painelVideo.SetActive(false);
    }
}
