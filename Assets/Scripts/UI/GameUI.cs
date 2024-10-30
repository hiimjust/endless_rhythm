using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private NotesJudgement notesJudgement;
    [SerializeField] private GameObject[] notifications;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        notesJudgement.UpdateUI.AddListener(UpdateScoreAndCombo);
        notesJudgement.NoteJudgementNotificationEvent.AddListener(DisplayJudgement);
    }

    private void OnDisable()
    {
        notesJudgement.UpdateUI.RemoveListener(UpdateScoreAndCombo);
        notesJudgement.NoteJudgementNotificationEvent.RemoveListener(DisplayJudgement);
    }

    private void UpdateScoreAndCombo()
    {
        comboText.text = GameManager.Instance.combo.ToString();
        scoreText.text = GameManager.Instance.score.ToString();
    }


    private void DisplayJudgement(int judgement)
    {
        for (int i = 0; i < notifications.Length; i++)
        {
            if (i == judgement)
                notifications[i].SetActive(true);
            else
                notifications[i].SetActive(false);
        }
    }
}
