using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    [SerializeField] float delayBeforScene = 2.1f;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        FindObjectOfType<Score>().ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(DelayBeforeNextScene());
    }

    public void Quit()
    {
       Application.Quit();
    }

    IEnumerator DelayBeforeNextScene()
    {
        yield return new WaitForSeconds(delayBeforScene);
        SceneManager.LoadScene(2);
    }
}
