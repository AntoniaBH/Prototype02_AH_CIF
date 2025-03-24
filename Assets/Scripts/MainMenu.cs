using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoRawImage;
    public Image transitionImage;

    private void Start()
    {
        Color color = transitionImage.color;
        color.a = 0f;
        transitionImage.color = color;
        transitionImage.raycastTarget = false;
    }

    public void PlayGame()
    {
        StartCoroutine(PlayVideoAndStartGame());
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private IEnumerator PlayVideoAndStartGame()
    {
        videoRawImage.gameObject.SetActive(true);
        videoPlayer.Play();

        yield return new WaitWhile(() => videoPlayer.isPlaying);

        yield return StartCoroutine(FadeToBlack(1.5f));

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

    private IEnumerator FadeToBlack(float duration)
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.raycastTarget = true;
        Color color = transitionImage.color;
        color.a = 0f;
        transitionImage.color = color;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            transitionImage.color = color;
            yield return null;
        }

        color.a = 1f;
        transitionImage.color = color;
    }
}