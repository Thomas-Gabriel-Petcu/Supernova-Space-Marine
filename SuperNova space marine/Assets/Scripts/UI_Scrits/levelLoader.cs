using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    #region DeveloperTool
    public void Start()
    {
        Debug.LogError("Attention! Developer tool on 'levelLoader' script still in place! Pressing Shift+E will load scene 2");
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneLoad(2);
            }
        }
    }
    #endregion
    public void SceneLoad(int sceneIndex)
    {
        Time.timeScale = 1;
        StartCoroutine(Load(sceneIndex));
    }

    IEnumerator Load(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
