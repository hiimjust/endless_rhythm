using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class BackgroundVFX : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start() {
        int index = Random.Range(0, Constants.BACKGROUND_VFX_COUNTER);
        videoPlayer.clip = (VideoClip)Resources.Load($"{Paths.BACKGROUND_VFX_PATH}{index}");
        videoPlayer.SetDirectAudioMute(0, true);
        videoPlayer.SetDirectAudioVolume(0, 0);
    }
}
