using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject buttonsList;
    [SerializeField] private GameObject settingsPanel;

    private void Start() {
        buttonsList.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void LoadMusicSelectScene()
    {
        GameManager.Instance.nextScene = Scenes.MUSIC_SELECT_SCENE;
        SceneManager.LoadScene(Scenes.LOADING_SCENE);
    }

    public void OpenSettingsPanel()
    {
        buttonsList.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        buttonsList.SetActive(true);
        settingsPanel.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }
}