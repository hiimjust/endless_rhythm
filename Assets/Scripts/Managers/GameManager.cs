using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Score Multiplier")]
    public float maxScore = Constants.MAX_SCORE;
    public float ratioScore;

    [Header("Current Song Info")]
    public int songID;
    public Sprite songSprite;
    public float noteSpeed = Constants.NOTE_SPEED;

    [Header("Current Song Checkpoint")]
    public bool play = false;
    public float startTime;
    public float timePerBeat;

    [Header("Ingame state")]
    public bool pause = false;

    [Header("Current Player's Combo & Score")]
    public int combo;
    public int score;

    [Header("Current Player's Combo Info")]
    public int perfect;
    public int good;
    public int hit;
    public int miss;

    [Header("Scenes")]
    public string nextScene = Scenes.START_GAME_SCENE;

    //Singleton GameManager
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Reset()
    {
        //Game started?
        play = false;
        pause = false;
        startTime = 0;
        //Scores & combos
        maxScore = Constants.MAX_SCORE;
        ratioScore = 0;
        combo = 0;
        score = 0;
        perfect = 0;
        good = 0;
        hit = 0;
        miss = 0;
        //Song info
        songID = 0;
        songSprite = null;
        noteSpeed = Constants.NOTE_SPEED;
        timePerBeat = 0;
        //Next scene
        nextScene = Scenes.START_GAME_SCENE;
    }

    public void ResetData()
    {
        Reset();
    }
}
