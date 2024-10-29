using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private SongDatabase database;
    private AudioSource source;
    private AudioClip song;
    private string songName;
    private bool isPlayed;

    private void Start() {
        GameManager.instance.start = false;
        songName = database.songData[GameManager.instance.songID].songName;
        source = GetComponent<AudioSource>();
        song = (AudioClip)Resources.Load(Constants.MUSIC_PATH + songName);
        isPlayed = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayed) {
            GameManager.instance.start = true;
            GameManager.instance.startTime = Time.time;
            isPlayed = true;
            source.PlayOneShot(song);
        }
    }
}
