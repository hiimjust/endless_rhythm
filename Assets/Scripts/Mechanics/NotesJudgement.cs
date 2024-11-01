using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NotesJudgement : MonoBehaviour
{
    [Header("Notes")]
    [SerializeField] private NotesManager notesManager;

    [SerializeField] private GameObject finish;

    [Header("Audio Source")]
    private AudioSource hitSource;
    [SerializeField] private AudioClip hitSound;

    private float endTime = 0;

    public UnityEvent<int> OnNoteJudgementNotificationEvent;
    public UnityEvent OnUpdateUI;
    public UnityEvent OnFinishGame;

    private void Start()
    {
        hitSource = GetComponent<AudioSource>();
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
    }

    private void Update()
    {
        if (notesManager.NotesTime.Count == 0)
        {
            if (Time.time > endTime + GameManager.Instance.startTime)
            {
                FinishGame();
            }
            return;
        }

        if (GameManager.Instance.start)
        {
            HandleInput();
            if (notesManager.NotesTime.Count != 0 && Time.time > notesManager.NotesTime[0] + GameManager.Instance.startTime + (GameManager.Instance.timePerBeat / 2))
            {
                NoteMiss();
            }
        }
    }

    private void HandleInput()
    {
        for (int i = 0; i < Configurations.KEYCODE_SETTINGS.Length; i++)
        {
            if (Input.GetKeyDown(Configurations.KEYCODE_SETTINGS[i]))
            {
                for (int j = 0; j < notesManager.Tiles.Count; j++)
                {
                    if (notesManager.Tiles[j] == i)
                    {
                        NoteJudgement(Mathf.Abs(Time.time - (notesManager.NotesTime[j] + GameManager.Instance.startTime)), j);
                        break;
                    }
                }
            }
        }
    }

    private void NoteMiss()
    {
        OnNoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_MISS);
        GameManager.Instance.miss++;
        GameManager.Instance.combo = 0;
        ProcessNote(0);
    }

    void NoteJudgement(float timeLag, int numOffset)
    {
        hitSource.PlayOneShot(hitSound);
        if (timeLag <= GameManager.Instance.timePerBeat / 8)
        {
            OnNoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_PERFECT);
            ProcessCombo(5);
            GameManager.Instance.perfect++;
            ProcessNote(numOffset);
        }
        else if (timeLag <= GameManager.Instance.timePerBeat / 4)
        {
            OnNoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_GOOD);
            ProcessCombo(3);
            GameManager.Instance.good++;
            ProcessNote(numOffset);
        }
        else if (timeLag <= GameManager.Instance.timePerBeat / 2)
        {
            OnNoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_HIT);
            ProcessCombo(1);
            GameManager.Instance.hit++;
            ProcessNote(numOffset);
        }
    }

    private void ProcessNote(int numOffset)
    {
        DeleteNoteData(numOffset);
        CalculateScore();
    }

    private void ProcessCombo(int ratio)
    {
        GameManager.Instance.ratioScore += ratio;
        GameManager.Instance.combo++;
    }

    private void DeleteNoteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.Tiles.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
    }

    private void CalculateScore()
    {
        GameManager.Instance.score = (int)Math.Round(1000000 * Math.Floor(GameManager.Instance.ratioScore / GameManager.Instance.maxScore * 1000000) / 1000000);
        OnUpdateUI?.Invoke();
    }

    private void FinishGame()
    {
        OnFinishGame?.Invoke();
    }
}
