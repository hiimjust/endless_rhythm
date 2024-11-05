using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AudioSource source;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Hide();
            else
                Show();
        }
    }

    public void Show()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        source.Pause();
        isPaused = true;
    }

    public void Hide()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        source.UnPause();
        isPaused = false;
    }

    public void StartMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetData();
        GameManager.Instance.nextScene = Scenes.START_GAME_SCENE;
        SceneManager.LoadScene(Scenes.LOADING_SCENE); 
    }

    public void MusicSelectionsMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetData();
        GameManager.Instance.nextScene = Scenes.MUSIC_SELECT_SCENE;
        SceneManager.LoadScene(Scenes.LOADING_SCENE);
    }
}
