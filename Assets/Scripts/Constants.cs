using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    #region CONSTANTS
    public static int MAX_SCORE = 3000;
    public static float NOTE_SPEED = 8f;

    public static float NOTE_JUDGEMENT_Z_POS = -13f;

    public static int NOTE_NOTIFICATION_PERFECT = 1;
    public static int NOTE_NOTIFICATION_GREAT = 2;
    public static int NOTE_NOTIFICATION_BAD = 3;
    public static int NOTE_NOTIFICATION_MISS = 0;

    #endregion

    #region STRINGS
    public static string MUSIC_SELECT = "MusicSelect";
    public static string GAME_SCENE = "GameScene";
    public static string RESULT_SCENE = "ResultScene";
    #endregion
}
