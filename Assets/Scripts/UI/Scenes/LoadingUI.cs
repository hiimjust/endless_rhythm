using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = null;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private Slider slider;

    private void Start()
    {
        sceneToLoad = GameManager.Instance.nextScene;
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            int index = Random.Range(0, Tips.TIPS.Length);
            slider.value = progress;
            loadingText.text = $"Loading... {progress * 100}%";
            instructionText.text = $"Tips: {Tips.TIPS[index]}";
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(3f);
                operation.allowSceneActivation = true;
            }
            yield return null; // Wait for 1 frame
        }
    }
}
