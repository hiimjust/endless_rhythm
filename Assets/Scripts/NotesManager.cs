using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataInfo
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public NoteInfo[] notes;

}
[Serializable]
public class NoteInfo
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    public int noteNum;
    private string songName;

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed;
    [SerializeField] private GameObject noteObj;
    [SerializeField] private SongDatabase database;

    private void OnEnable()
    {
        NotesSpeed = GameManager.instance.noteSpeed;
        noteNum = 0;
        songName = database.songData[GameManager.instance.songID].songName;
        LoadSong(songName);
    }

    private void LoadSong(string songName)
    {
        string inputString = Resources.Load<TextAsset>("Beatmaps/" + songName).ToString();
        DataInfo inputJSON = JsonUtility.FromJson<DataInfo>(inputString);

        noteNum = inputJSON.notes.Length;

        for (int i = 0; i < inputJSON.notes.Length; i++)
        {
            float kankaku = 60 / (inputJSON.BPM * (float)inputJSON.notes[i].LPB);
            float beatSec = kankaku * (float)inputJSON.notes[i].LPB;
            float time = (beatSec * inputJSON.notes[i].num / (float)inputJSON.notes[i].LPB) + inputJSON.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJSON.notes[i].block);
            NoteType.Add(inputJSON.notes[i].type);

            float z = NotesTime[i] * NotesSpeed - 13f;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJSON.notes[i].block - 1.5f, 0.5f, z), Quaternion.identity));
        }
    }
}
