using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotesJudgement : MonoBehaviour
{
    [Header("Notes")]
    [SerializeField] private NotesManager notesManager;

    [Header("UI")]
    [SerializeField] private GameObject[] notifications;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject finish;

    [Header("Audio Source")]
    private AudioSource hitSource;
    [SerializeField] private AudioClip hitSound;

    private float endTime = 0;

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

        if (GameManager.instance.start)
        {
            HandleInput();

            if (Time.time > endTime + GameManager.instance.startTime)
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
        KeyCode[] keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };

        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                for (int j = 0; j < notesManager.LaneNum.Count; j++)
                {
                    if (notesManager.LaneNum[j] == i)
                    {
                        NoteJudgement(Mathf.Abs(Time.time - (notesManager.NotesTime[j] + GameManager.instance.startTime)), j);
                        break;
                    }
                }
            }
        }
    }

    private void NoteMiss()
    {
        if (Time.time > notesManager.NotesTime[0] + GameManager.instance.startTime + (GameManager.instance.TimePerBeat / 2))
        {
            NoteJudgementNotification(3);
            GameManager.instance.miss++;
            GameManager.instance.combo = 0;
            ProcessNote(0);
        }
    }

    private void NoteHit(int numOffset)
    {

    }

    void NoteJudgement(float timeLag, int numOffset)
    {
        hitSource.PlayOneShot(hitSound);
        if (timeLag <= GameManager.instance.TimePerBeat / 8)
        {
            NoteJudgementNotification(0);
            GameManager.instance.ratioScore += 5;
            GameManager.instance.perfect++;
            GameManager.instance.combo++;
            ProcessNote(numOffset);
        }
        else if (timeLag <= GameManager.instance.TimePerBeat / 4)
        {
            NoteJudgementNotification(1);
            GameManager.instance.ratioScore += 3;
            GameManager.instance.great++;
            GameManager.instance.combo++;
            ProcessNote(numOffset);
        }
        else if (timeLag <= GameManager.instance.TimePerBeat / 2)
        {
            NoteJudgementNotification(2);
            GameManager.instance.ratioScore += 1;
            GameManager.instance.bad++;
            GameManager.instance.combo++;
            ProcessNote(numOffset);
        }
    }


    private void ProcessNote(int numOffset)
    {
        DeleteNoteData(numOffset);
        CalculateScore();
    }

    private void NoteJudgementNotification(int judge)
    {
        Instantiate(notifications[judge], new Vector3(notesManager.LaneNum[0] - 1.5f, 1f, -12f), Quaternion.Euler(45, 0, 0));
    }

    private void DeleteNoteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
    }

    private void CalculateScore()
    {
        GameManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GameManager.instance.ratioScore / GameManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GameManager.instance.combo.ToString();
        scoreText.text = GameManager.instance.score.ToString();
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
