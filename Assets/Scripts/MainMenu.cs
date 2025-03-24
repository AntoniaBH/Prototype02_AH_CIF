using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoRawImage;
    public Image transitionImage;

    //Quit Window Elements
    public GameObject quitWindow;
    public RectTransform quitPolaroid;
    public RectTransform quitOptionsWindow;

    //Start Window Elements
    public GameObject startWindow;
    public RectTransform startPolaroid;
    public RectTransform startOptionsWindow;

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

    public void ButtonQuitGame()
    {
        StartCoroutine(QuitGame());
    }

    public void StartQuitTransition()
    {
        StartCoroutine(QuitWindowTransition());
    }

    public void StartStartTransition()
    {
        StartCoroutine(StartWindowTransition());
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

    private IEnumerator QuitGame()
    {
        yield return StartCoroutine(FadeToBlack(1f));

        Debug.Log("Quit");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private IEnumerator QuitWindowTransition()
    {
        quitWindow.SetActive(true);

        yield return StartCoroutine(SlideIn(quitPolaroid, new Vector2(168, -8), new Vector2(Screen.width + 600f, -8), 2f));

        yield return StartCoroutine(SlideIn(quitOptionsWindow, new Vector2(-567, 11), new Vector2(-905f, 11), 1f));
    }

    private IEnumerator StartWindowTransition()
    {
        startWindow.SetActive(true);

        yield return StartCoroutine(SlideIn(startPolaroid, new Vector2(-175, -8), new Vector2(-200f, -8), 1f));

        yield return StartCoroutine(SlideIn(startOptionsWindow, new Vector2(-5, 0), new Vector2(Screen.width + 200f, 0), 1f));
    }

    private IEnumerator SlideIn(RectTransform rectTransform, Vector2 targetPosition, Vector2 startPosition, float duration)
    {
        rectTransform.anchoredPosition = startPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }
}