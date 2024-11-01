using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MusicSelectionUI : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private SongDatabase database;

    [Header("Songs List")]
    [SerializeField] private Transform content;
    [SerializeField] private SongInfo songDisplayPrefab;
    [SerializeField] private List<SongInfo> songDisplayInfos;

    [Header("Selected Song Info")]
    [SerializeField] private Image songImage;
    [SerializeField] private TextMeshProUGUI songNameText;
    [SerializeField] private TextMeshProUGUI songArtistsText;
    [SerializeField] private TextMeshProUGUI songBPMText;
    [SerializeField] private TextMeshProUGUI songLevelText;

    [Header("Selected Song Info")]
    [SerializeField] private TextMeshProUGUI instructionText;

    private AudioSource source;
    private AudioClip music;
    private string songTitle;
    private int select;

    private void Start()
    {
        select = 0;
        source = GetComponent<AudioSource>();
        songTitle = database.songData[select].songTitle;
        music = (AudioClip)Resources.Load(Paths.MUSIC_PATH + songTitle);
        CreateSongsList();
        UpdateSongsList();
        StartCoroutine(UpdateInstructionText());
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
        if (Input.GetKeyDown(KeyCode.Return))
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
            CreateSong(database.songData[i].songName, database.songData[i].songLevel);
        }
    }

    private void CreateSong(string songName, string songLevel)
    {
        SongInfo songInfo = Instantiate(songDisplayPrefab, content);
        songInfo.SongName.text = songName;
        songInfo.SongLevel.text = songLevel;
        songInfo.Background.color = Color.gray;
        songDisplayInfos.Add(songInfo);
        Button songButton = songInfo.GetComponentInChildren<Button>();
        if (songButton != null)
        {
            int index = songDisplayInfos.Count - 1;
            songButton.onClick.AddListener(() => SelectSongByClick(index));
        }
    }

    private void UpdateSongsList()
    {
        songTitle = database.songData[select].songTitle;
        music = (AudioClip)Resources.Load($"{Paths.MUSIC_PATH}{songTitle}");
        if (music == null)
        {
            Debug.LogError($"Cannot find {songTitle} at {Paths.MUSIC_PATH}");
            return;
        }
        source.Stop();
        source.PlayOneShot(music);
        DisplaySelectedSong();
    }

    private void DisplaySelectedSong()
    {
        songImage.transform.localRotation = Quaternion.identity;
        songImage.sprite = database.songData[select].songImage;
        songNameText.text = database.songData[select].songName;
        songArtistsText.text = database.songData[select].artists;
        songBPMText.text = $"BPM: {database.songData[select].BPM}";
        songLevelText.text = $"Level: {database.songData[select].songLevel}";
        SelectedSongBackroundColor(Color.green);
    }

    private void SelectedSongBackroundColor(Color color)
    {
        songDisplayInfos[select].Background.color = color;
    }

    public void RandomAnySong()
    {
        SelectedSongBackroundColor(Color.gray);
        int previousSelect = select;
        do
        {
            select = UnityEngine.Random.Range(0, database.songData.Length);
        } while (previousSelect == select);
        UpdateSongsList();
    }

    private void SelectSongByClick(int index)
    {
        if (index == select) return;
        SelectedSongBackroundColor(Color.gray);
        select = index;
        UpdateSongsList();
    }

    private IEnumerator UpdateInstructionText()
    {
        while (true)
        {
            int index = UnityEngine.Random.Range(0, Tips.TIPS.Length);
            instructionText.text = Tips.TIPS[index];
            yield return new WaitForSeconds(10f);
            instructionText.text = string.Empty;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartSong()
    {
        GameManager.Instance.songID = select;
        GameManager.Instance.songSprite = database.songData[select].songImage;
        GameManager.Instance.nextScene = Scenes.GAME_SCENE;
        SceneManager.LoadScene(Scenes.LOADING_SCENE);
    }

    public void Back()
    {
        SceneManager.LoadScene(Scenes.START_GAME_SCENE);
    }
}
