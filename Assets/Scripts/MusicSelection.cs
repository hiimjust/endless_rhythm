using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MusicSelection : MonoBehaviour
{
    [SerializeField] private SongDatabase database;
    [SerializeField] private Image songImage;

    [SerializeField] private Transform content;

    [SerializeField] private SongDisplayInfo songDisplayPrefab;
    [SerializeField] private List<SongDisplayInfo> songDisplayInfos;

    private AudioSource source;
    private AudioClip music;
    private string songName;

    private int select;

    private void Awake()
    {

    }

    private void Start()
    {
        select = 0;
        source = GetComponent<AudioSource>();
        songName = database.songData[select].songName;
        music = (AudioClip)Resources.Load(Constants.MUSIC_PATH + songName);
        CreateSongsList();
        UpdateSongsList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < database.songData.Length)
            {
                SelectedSongBackroundColor(Color.gray);
                select++;
                UpdateSongsList();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0)
            {
                SelectedSongBackroundColor(Color.gray);
                select--;
                UpdateSongsList();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSong();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    private void CreateSongsList()
    {

        for (int i = 0; i < database.songData.Length; i++)
        {
            SongDisplayInfo songInfo = Instantiate(songDisplayPrefab, content);
            songInfo.SongName.text = database.songData[i].songName;
            songInfo.SongLevel.text = database.songData[i].songLevel.ToString();
            songInfo.Background.color = Color.gray;
            songDisplayInfos.Add(songInfo);
        }
    }


    private void UpdateSongsList()
    {
        songName = database.songData[select].songName;
        music = (AudioClip)Resources.Load(Constants.MUSIC_PATH + songName);
        source.Stop();
        source.PlayOneShot(music);
        DisplaySelectedSong();
    }

    private void DisplaySelectedSong()
    {
        songImage.sprite = database.songData[select].songImage;
        SelectedSongBackroundColor(Color.green);
    }

    private void SelectedSongBackroundColor(Color color)
    {
        songDisplayInfos[select].Background.color = color;
    }

    private void StartSong()
    {
        GameManager.Instance.songID = select;
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }

    private void Back()
    {
        SceneManager.LoadScene(Constants.START_GAME_SCENE);
    }
}
