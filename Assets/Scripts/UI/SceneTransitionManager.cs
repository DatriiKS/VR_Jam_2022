using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreenObject;

    public void StartButton(string sceneName)
    {
        GoToScene(sceneName);

        // foreach (var sceneName in scenes)
        // {
        //     SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        // }
    }

    public void GoToScene(string sceneName)
    {
        StartCoroutine(GoToSceneRoutine(sceneName));
    }
    IEnumerator GoToSceneRoutine(string sceneName)
    {
        fadeScreenObject.FadeOut();

        // Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreenObject.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
