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
        scoreText.text = GameManager.instance.score.ToString();
        perfectText.text = GameManager.instance.perfect.ToString();
        goodText.text = GameManager.instance.good.ToString();
        hitText.text = GameManager.instance.hit.ToString();
        missText.text = GameManager.instance.miss.ToString();
    }

    public void Retry()
    {
        GameManager.instance.perfect = 0;
        GameManager.instance.good = 0;
        GameManager.instance.hit = 0;
        GameManager.instance.miss = 0;
        GameManager.instance.ratioScore = 0;
        GameManager.instance.score = 0;
        GameManager.instance.combo = 0;
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }
}
