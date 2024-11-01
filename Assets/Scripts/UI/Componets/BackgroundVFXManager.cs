using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class BackgroundVFXManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private int counter;

    private void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
        counter = Constants.BACKGROUND_VFX_COUNTER;
    }

    private void Start() {
        int index = Random.Range(0, counter);
        videoPlayer.clip = (VideoClip)Resources.Load(Paths.BACKGROUND_VFX_PATH + index.ToString());
        videoPlayer.SetDirectAudioMute(0, true);
        videoPlayer.SetDirectAudioVolume(0, 0);
    }
}
