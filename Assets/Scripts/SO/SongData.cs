using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "Song/SongData")]
public class SongData : ScriptableObject
{
    [SerializeField] public string songID;
    [SerializeField] public string songTitle;
    [SerializeField] public string songName;
    [SerializeField] public string artists;
    [SerializeField] public string BPM;
    [SerializeField] public string songLevel;
    [SerializeField] public Sprite songImage;
}
