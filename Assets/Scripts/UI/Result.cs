using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI perfectText;
    [SerializeField] private TextMeshProUGUI goodText;
    [SerializeField] private TextMeshProUGUI hitText;
    [SerializeField] private TextMeshProUGUI missText;

    private void OnEnable()
    {
        scoreText.text = GameManager.Instance.score.ToString();
        perfectText.text = GameManager.Instance.perfect.ToString();
        goodText.text = GameManager.Instance.good.ToString();
        hitText.text = GameManager.Instance.hit.ToString();
        missText.text = GameManager.Instance.miss.ToString();
    }

    public void Retry()
    {
        GameManager.Instance.perfect = 0;
        GameManager.Instance.good = 0;
        GameManager.Instance.hit = 0;
        GameManager.Instance.miss = 0;
        GameManager.Instance.ratioScore = 0;
        GameManager.Instance.score = 0;
        GameManager.Instance.combo = 0;
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }
}
