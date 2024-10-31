using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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
        if (Input.GetKeyDown(KeyCode.DownArrow) && select < database.songData.Length - 1)
        {
            SelectedSongBackroundColor(Color.gray);
            select++;
            UpdateSongsList();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && select > 0)
        {
            SelectedSongBackroundColor(Color.gray);
            select--;
            UpdateSongsList();
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
        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * database.songData.Length);
        for (int i = 0; i < database.songData.Length; i++)
        {
            CreateSong(database.songData[i].songName, database.songData[i].songLevel.ToString());
        }
    }

    private void CreateSong(string songName, string songLevel)
    {
        SongDisplayInfo songInfo = Instantiate(songDisplayPrefab, content);
        songInfo.SongName.text = songName;
        songInfo.SongLevel.text = songLevel;
        songInfo.Background.color = Color.gray;
        songDisplayInfos.Add(songInfo);
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
        songImage.transform.localRotation = Quaternion.identity;
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
