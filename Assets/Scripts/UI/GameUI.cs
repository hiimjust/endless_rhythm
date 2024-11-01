using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private NotesJudgement notesJudgement;
    [SerializeField] private GameObject[] notifications;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI perfect;
    [SerializeField] private TextMeshProUGUI good;
    [SerializeField] private TextMeshProUGUI hit;
    [SerializeField] private TextMeshProUGUI miss;

    [SerializeField] private Image songImage;

    [SerializeField] private GameObject finish;


    private void OnEnable()
    {
        notesJudgement.OnUpdateUI.AddListener(UpdateScoreAndCombo);
        notesJudgement.OnNoteJudgementNotificationEvent.AddListener(DisplayJudgement);
        notesJudgement.OnFinishGame.AddListener(Finish);
    }

    private void OnDisable()
    {
        notesJudgement.OnUpdateUI.RemoveListener(UpdateScoreAndCombo);
        notesJudgement.OnNoteJudgementNotificationEvent.RemoveListener(DisplayJudgement);
        notesJudgement.OnFinishGame.RemoveListener(Finish);
    }

    private void Start() {
        songImage.sprite = GameManager.Instance.songSprite;
    }

    private void UpdateSongImage(Image img)
    {
        songImage.sprite = img.sprite;
    }

    private void UpdateScoreAndCombo()
    {
        comboText.text = GameManager.Instance.combo.ToString();
        scoreText.text = GameManager.Instance.score.ToString();
        perfect.text = GameManager.Instance.perfect.ToString();
        good.text = GameManager.Instance.good.ToString();
        hit.text = GameManager.Instance.hit.ToString();
        miss.text = GameManager.Instance.miss.ToString();
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

    private void Finish()
    {
        finish.SetActive(true);
        Invoke(Scenes.RESULT_SCENE, 5f);
        return;
    }

    private void ResultScene()
    {
        SceneManager.LoadScene(Scenes.RESULT_SCENE);
    }
}
