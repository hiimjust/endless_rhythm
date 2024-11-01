using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance {
        get {
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
    public bool start;
    public float startTime;
    public float timePerBeat;

    [Header("Current Player's Combo & Score")]
    public int combo;
    public int score;

    [Header("Current Player's Combo Info")]
    public int perfect;
    public int good;
    public int hit;
    public int miss;

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
}
