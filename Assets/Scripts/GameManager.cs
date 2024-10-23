using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region PART5
    public float maxScore;
    public float ratioScore;
    #endregion

    public int songID;
    public float noteSpeed;

    public bool Start;
    public float StartTime;

    public int combo;
    public int score;

    public int perfect;
    public int great;
    public int good;
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
