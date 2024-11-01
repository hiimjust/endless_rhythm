using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    private int noteNumber;
    private string songTitle;

    [SerializeField] private float noteSpeed;
    [SerializeField] private GameObject noteObj;
    [SerializeField] private SongDatabase database;

    public List<int> Tiles = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    private void OnEnable()
    {
        noteSpeed = GameManager.Instance.noteSpeed;
        noteNumber = 0;
        songTitle = database.songData[GameManager.Instance.songID].songTitle;
        LoadSong(songTitle);
    }

    private void LoadSong(string songName)
    {
        string inputString = Resources.Load<TextAsset>($"{Paths.BEATMAP_PATH}{songName}").ToString();
        DataInfo inputJSON = JsonUtility.FromJson<DataInfo>(inputString);
        LoadBeatmap(inputJSON);
    }

    private void LoadBeatmap(DataInfo inputJSON)
    {
        noteNumber = inputJSON.notes.Length;

        for (int i = 0; i < inputJSON.notes.Length; i++)
        {
            float line = 60 / (inputJSON.BPM * (float)inputJSON.notes[i].LPB);
            float beat = line * (float)inputJSON.notes[i].LPB;
            GameManager.Instance.timePerBeat = beat;
            float time = (beat * inputJSON.notes[i].num / (float)inputJSON.notes[i].LPB) + inputJSON.offset * 0.01f;
            NotesTime.Add(time);
            Tiles.Add(inputJSON.notes[i].block);
            NoteType.Add(inputJSON.notes[i].type);
            float z = NotesTime[i] * noteSpeed + Constants.NOTE_JUDGEMENT_Z_POS;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJSON.notes[i].block - 1.5f, 0.5f, z), Quaternion.identity));
        }
    }
}
