using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public float transitionTimeRewind = 0.5f;
    public AudioSource clickSound;

    public void QuitGame()
    {
        Application.Quit();
        clickSound.Play();
    }


    public void PlayPress()
    {
        StartCoroutine(LoadLevel_NormalTransition(1));
        clickSound.Play();
        GameManager.numberOfItemsDone = 0;
        GameManager.gameOver = false;
    }

    public void RePlayPress()
    {
        StartCoroutine(LoadLevel_NormalTransition(1));
        clickSound.Play();
        GameManager.numberOfItemsDone = 0;
        GameManager.gameOver = false;
        GameManager.isLevelRepeat = true;
    }


    public void BackToMenuPress()
    {
        StartCoroutine(LoadLevel_NormalTransition(0));
        GameManager.isLevelRepeat = true;
        clickSound.Play();
    }

    IEnumerator LoadLevel_NormalTransition(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
