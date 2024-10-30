using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMusicSelectScene()
    {
        SceneManager.LoadScene(Constants.MUSIC_SELECT_SCENE);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
