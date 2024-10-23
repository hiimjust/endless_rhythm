using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteJudgement : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] private NotesManager notesManager;

    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject finish;

    private AudioSource hitSource;
    [SerializeField] AudioClip hitSound;

    private float endTime = 0;

    private void Start()
    {
        hitSource = GetComponent<AudioSource>();
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
        Debug.Log("EndTime = " + endTime);
    }

    private void Update()
    {
        if (GameManager.instance.Start && !(notesManager.NotesTime.Count == 0))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (notesManager.LaneNum[0] == 0)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 0)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 1)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 2)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.LaneNum[0] == 3)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 3)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.StartTime)), 1);
                    }
                }
            }

            if (Time.time > endTime + GameManager.instance.StartTime)
            {
                finish.SetActive(true);
                Invoke("ResultScene", 3f);
                return;
            }

            if (Time.time > notesManager.NotesTime[0] + GameManager.instance.StartTime + 0.2f)
            {
                message(3);
                deleteData(0);
                GameManager.instance.miss++;
                GameManager.instance.combo = 0;
                Debug.Log("Miss");
            }
        }
    }
    void Judgement(float timeLag, int numOffset)
    {
        hitSource.PlayOneShot(hitSound);
        if (timeLag <= 0.05f)
        {
            Debug.Log("Perfect");
            message(0);
            GameManager.instance.ratioScore += 5;
            GameManager.instance.perfect++;
            GameManager.instance.combo++;
            deleteData(numOffset);
        }
        else
        {
            if (timeLag <= 0.08f)
            {
                Debug.Log("Great");
                message(1);
                GameManager.instance.ratioScore += 3;
                GameManager.instance.great++;
                GameManager.instance.combo++;
                deleteData(numOffset);
            }
            else
            {
                if (timeLag <= 0.1f)
                {
                    Debug.Log("Bad");
                    message(2);
                    GameManager.instance.ratioScore += 1;
                    GameManager.instance.bad++;
                    GameManager.instance.combo++;
                    deleteData(numOffset);
                }
            }
        }
    }
    float GetABS(float num)
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }

    void deleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        GameManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GameManager.instance.ratioScore / GameManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GameManager.instance.combo.ToString();
        scoreText.text = GameManager.instance.score.ToString();
    }

    void message(int judge)
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.LaneNum[0] - 1.5f, -0.5f, -15f), Quaternion.Euler(45, 0, 0));
    }

    void ResultScene() {
        SceneManager.LoadScene("ResultScene");
    }
}
