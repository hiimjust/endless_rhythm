using System;
using System.Collections;
using System.Collections.Generic;
using NoteEditor.Model;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelection : MonoBehaviour
{
    [SerializeField] SongDatabase database;
    [SerializeField] TextMeshProUGUI[] songNameText;
    [SerializeField] TextMeshProUGUI[] songLevelText;
    [SerializeField] Image songImage;

    private AudioSource source;
    private AudioClip music;
    private string songName;

    private int select;

    private void Start()
    {
        select = 0;
        source = GetComponent<AudioSource>();
        songName = database.songData[select].songName;
        music = (AudioClip)Resources.Load("BGMs/" + songName);
        SongUpdateAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < database.songData.Length)
            {
                select++;
                SongUpdateAll();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0)
            {
                select--;
                SongUpdateAll();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SongStart();
        }
    }

    private void SongStart()
    {
        GameManager.instance.songID = select;
        Debug.Log("Enter");
        SceneManager.LoadScene("GameScene");
    }

    private void SongUpdateAll()
    {
        songName = database.songData[select].songName;
        music = (AudioClip)Resources.Load("BGMs/" + songName);
        source.Stop();
        source.PlayOneShot(music);
        for (int i = 0; i < 5; i++)
        {
            SongUpdate(i - 2);
        }
    }

    private void SongUpdate(int id)
    {
        try
        {
            songNameText[id + 2].text = database.songData[select + id].songName;
            songLevelText[id + 2].text = "Lv." + database.songData[select + id].songLevel;
        }
        catch
        {
            songNameText[id + 2].text = "";
            songLevelText[id + 2].text = "";
        }
        if (id == 0)
        {
            songImage.sprite = database.songData[select + id].songImage;
        }
    }
}
