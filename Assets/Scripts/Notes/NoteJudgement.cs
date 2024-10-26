using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteJudgement : MonoBehaviour
{
    [Header("Notes")]
    [SerializeField] private NotesManager notesManager;

    [Header("UI")]
    [SerializeField] private GameObject[] notificationObj;
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
        Debug.Log("EndTime = " + endTime);
    }

    private void Update()
    {
        if (notesManager.NotesTime.Count == 0)
        {
            return;
        }

        if (GameManager.instance.start)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (notesManager.LaneNum[0] == 0)
                {
                    Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 0)
                    {
                        Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 1)
                    {
                        Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 2)
                    {
                        Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.LaneNum[0] == 3)
                {
                    Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 3)
                    {
                        Judgement(Mathf.Abs(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
                    }
                }
            }

            if (Time.time > endTime + GameManager.instance.startTime)
            {
                finish.SetActive(true);
                Invoke(Constants.RESULT_SCENE, 5f);
                return;
            }


            if (notesManager.NotesTime.Count != 0)
            {
                if (Time.time > notesManager.NotesTime[0] + GameManager.instance.startTime + 0.2f)
                {
                    Debug.Log("Miss");
                    NoteJudgementNotification(3);
                    GameManager.instance.miss++;
                    GameManager.instance.combo = 0;
                    DeleteData(0);
                }
            }
        }
    }



    void Judgement(float timeLag, int numOffset)
    {
        hitSource.PlayOneShot(hitSound);
        if (timeLag <= 0.05f)
        {
            Debug.Log("Perfect");
            NoteJudgementNotification(0);
            GameManager.instance.ratioScore += 5;
            GameManager.instance.perfect++;
            GameManager.instance.combo++;
            DeleteData(numOffset);
        }
        else
        {
            if (timeLag <= 0.08f)
            {
                Debug.Log("Great");
                NoteJudgementNotification(1);
                GameManager.instance.ratioScore += 3;
                GameManager.instance.great++;
                GameManager.instance.combo++;
                DeleteData(numOffset);
            }
            else
            {
                if (timeLag <= 0.1f)
                {
                    Debug.Log("Bad");
                    NoteJudgementNotification(2);
                    GameManager.instance.ratioScore += 1;
                    GameManager.instance.bad++;
                    GameManager.instance.combo++;
                    DeleteData(numOffset);
                }
            }
        }
    }


    void DeleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        GameManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GameManager.instance.ratioScore / GameManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GameManager.instance.combo.ToString();
        scoreText.text = GameManager.instance.score.ToString();
    }

    void NoteJudgementNotification(int judge)
    {
        Instantiate(notificationObj[judge], new Vector3(notesManager.LaneNum[0] - 1.5f, -0.5f, -15f), Quaternion.Euler(45, 0, 0));
    }

    void ResultScene()
    {
        SceneManager.LoadScene(Constants.RESULT_SCENE);
    }
}
