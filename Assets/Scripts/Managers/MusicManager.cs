using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private SongDatabase database;
    private AudioSource source;
    private AudioClip song;
    private string songTitle;
    private bool isPlayed;

    public UnityEvent SongStartEvent;

    private void Start() {
        GameManager.Instance.start = false;
        songTitle = database.songData[GameManager.Instance.songID].songTitle;
        source = GetComponent<AudioSource>();
        song = (AudioClip)Resources.Load($"{Paths.MUSIC_PATH}{songTitle}");
        isPlayed = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayed) {
            GameManager.Instance.start = true;
            GameManager.Instance.startTime = Time.time;
            isPlayed = true;
            source.PlayOneShot(song);
            SongStartEvent?.Invoke();
        }
    }
}
