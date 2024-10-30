using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    #region CONSTANTS
    public static int MAX_SCORE = 3000;
    public static float NOTE_SPEED = 8f;

    public static float NOTE_JUDGEMENT_Z_POS = -13f;

    public static int NOTE_NOTIFICATION_PERFECT = 0;
    public static int NOTE_NOTIFICATION_GOOD = 1;
    public static int NOTE_NOTIFICATION_HIT = 2;
    public static int NOTE_NOTIFICATION_MISS = 3;

    #endregion

    #region STRINGS
    public static string MENU_SCENE = "MenuScene";
    public static string MUSIC_SELECT_SCENE = "MusicSelectScene";
    public static string GAME_SCENE = "GameScene";
    public static string RESULT_SCENE = "ResultScene";

    public static string MUSIC_PATH = "BGMs/";
    #endregion
}
