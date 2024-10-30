using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongDisplayInfo : MonoBehaviour
{
    [SerializeField] public Image Background;
    [SerializeField] public TextMeshProUGUI SongName;
    [SerializeField] public TextMeshProUGUI SongLevel;

    public SongDisplayInfo(string songName, string songLevel, Sprite background)
    {
        SongName.text = songName;
        SongLevel.text = songLevel;
        Background.sprite = background;
    }
}
