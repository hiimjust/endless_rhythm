using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private SongDatabase database;

    [SerializeField] private TextMeshProUGUI songName;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI perfectText;
    [SerializeField] private TextMeshProUGUI goodText;
    [SerializeField] private TextMeshProUGUI hitText;
    [SerializeField] private TextMeshProUGUI missText;
    [SerializeField] private Image songImage;

    private void OnEnable()
    {
        songName.text = database.songData[GameManager.Instance.songID].songName;
        songImage.sprite = database.songData[GameManager.Instance.songID].songImage;
        scoreText.text = GameManager.Instance.score.ToString();
        perfectText.text = GameManager.Instance.perfect.ToString();
        goodText.text = GameManager.Instance.good.ToString();
        hitText.text = GameManager.Instance.hit.ToString();
        missText.text = GameManager.Instance.miss.ToString();
    }

    public void Retry()
    {
        ResetData();
        SceneManager.LoadScene(Scenes.GAME_SCENE);
    }

    public void StartMenu()
    {
        ResetData();
        SceneManager.LoadScene(Scenes.START_GAME_SCENE);
    }

    public void MusicSelectionsMenu()
    {
        ResetData();
        SceneManager.LoadScene(Scenes.MUSIC_SELECT_SCENE);
    }

    private void ResetData()
    {
        GameManager.Instance.perfect = 0;
        GameManager.Instance.good = 0;
        GameManager.Instance.hit = 0;
        GameManager.Instance.miss = 0;
        GameManager.Instance.ratioScore = 0;
        GameManager.Instance.score = 0;
        GameManager.Instance.combo = 0;
    }
}
