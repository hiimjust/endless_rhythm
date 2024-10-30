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

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject finish;

    [Header("Audio Source")]
    private AudioSource hitSource;
    [SerializeField] private AudioClip hitSound;

    private KeyCode[] keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };
    private float endTime = 0;

    #region EVENTS
    public UnityEvent<int> NoteJudgementNotificationEvent;
    public UnityEvent UpdateUI;
    #endregion

    private void Start()
    {
        hitSource = GetComponent<AudioSource>();
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
    }

    private void Update()
    {
        if (notesManager.NotesTime.Count == 0)
        {
            return;
        }

        if (GameManager.Instance.start)
        {
            HandleInput();

            if (Time.time > endTime + GameManager.Instance.startTime)
            {
                FinishGame();
            }

            if (notesManager.NotesTime.Count != 0)
            {
                NoteMiss();
            }
        }
    }

    private void HandleInput()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                for (int j = 0; j < notesManager.LaneNum.Count; j++)
                {
                    if (notesManager.LaneNum[j] == i)
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
        if (Time.time > notesManager.NotesTime[0] + GameManager.Instance.startTime + (GameManager.Instance.timePerBeat / 2))
        {
            NoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_MISS);
            GameManager.Instance.miss++;
            GameManager.Instance.combo = 0;
            ProcessNote(0);
        }
    }

    void NoteJudgement(float timeLag, int numOffset)
    {
        hitSource.PlayOneShot(hitSound);
        if (timeLag <= GameManager.Instance.timePerBeat / 8)
        {
            NoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_PERFECT);
            GameManager.Instance.ratioScore += 5;
            GameManager.Instance.perfect++;
            GameManager.Instance.combo++;
            ProcessNote(numOffset);
        }
        else if (timeLag <= GameManager.Instance.timePerBeat / 4)
        {
            NoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_GOOD);
            GameManager.Instance.ratioScore += 3;
            GameManager.Instance.good++;
            GameManager.Instance.combo++;
            ProcessNote(numOffset);
        }
        else if (timeLag <= GameManager.Instance.timePerBeat / 2)
        {
            NoteJudgementNotificationEvent?.Invoke(Constants.NOTE_NOTIFICATION_HIT);
            GameManager.Instance.ratioScore += 1;
            GameManager.Instance.hit++;
            GameManager.Instance.combo++;
            ProcessNote(numOffset);
        }
    }

    private void ProcessNote(int numOffset)
    {
        DeleteNoteData(numOffset);
        CalculateScore();
    }

    private void DeleteNoteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
    }

    private void CalculateScore()
    {
        GameManager.Instance.score = (int)Math.Round(1000000 * Math.Floor(GameManager.Instance.ratioScore / GameManager.Instance.maxScore * 1000000) / 1000000);
        UpdateUI?.Invoke();
    }

    private void FinishGame()
    {
        finish.SetActive(true);
        Invoke(Constants.RESULT_SCENE, 5f);
        return;
    }

    private void ResultScene()
    {
        SceneManager.LoadScene(Constants.RESULT_SCENE);
    }
}
