using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] TextMeshProUGUI badText;
    [SerializeField] TextMeshProUGUI missText;
    
    private void OnEnable()
    {
        scoreText.text = GameManager.instance.score.ToString();
        perfectText.text = GameManager.instance.perfect.ToString();
        greatText.text = GameManager.instance.great.ToString();
        badText.text = GameManager.instance.bad.ToString();
        missText.text = GameManager.instance.miss.ToString();
    }

    public void Retry()
    {
        GameManager.instance.perfect = 0;
        GameManager.instance.great = 0;
        GameManager.instance.bad = 0;
        GameManager.instance.miss = 0;
        // GameManager.instance.maxScore = 0;
        GameManager.instance.ratioScore = 0;
        GameManager.instance.score = 0;
        GameManager.instance.combo = 0;
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }
}
