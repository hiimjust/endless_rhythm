using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Score Multiplier")]
    public float maxScore = Constants.MAX_SCORE;
    public float ratioScore;

    [Header("Current Song Info")]
    public int songID;
    public float noteSpeed = Constants.NOTE_SPEED;

    [Header("Current Song Checkpoint")]
    public bool start;
    public float startTime;
    public float TimePerBeat;

    [Header("Current Player's Combo & Score")]
    public int combo;
    public int score;

    [Header("Current Player's Combo Info")]
    public int perfect;
    public int great;
    public int bad;
    public int miss;

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
