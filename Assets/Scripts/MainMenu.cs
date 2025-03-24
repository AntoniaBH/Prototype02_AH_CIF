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

    [Header("Start Window Variables")]
    public GameObject startWindow;
    public Image startOverlayBackground;
    public RectTransform startPolaroid;
    public RectTransform startOptionsWindow;

    [Header("Start Window Speed")]
    public float startOverlaySpeed = 1f;
    public float startPolaroidSpeed = 1f;
    public float startOptionsSpeed = 1f;

    [Header("Quit Window Variables")]
    public GameObject quitWindow;
    public Image quitOverlayBackground;
    public RectTransform quitPolaroid;
    public RectTransform quitOptionsWindow;

    [Header("Quit Window Speed")]
    public float quitOverlaySpeed = 1f;
    public float quitPolaroidSpeed = 1f;
    public float quitOptionsSpeed = 1f;

    private void Start()
    {
        Color color = transitionImage.color;
        color.a = 0f;
        transitionImage.color = color;
        transitionImage.raycastTarget = false;

        startWindow.SetActive(false);
        quitWindow.SetActive(false);
    }

    public void PlayGame()
    {
        StartCoroutine(PlayVideoAndStartGame());
    }

    public void ButtonQuitGame()
    {
        StartCoroutine(QuitGame());
    }

   public void OnStartButton()
    {
        startWindow.SetActive(true);
        StartCoroutine(AnimateStartWindow(true));
    }

    public void OnQuitButton()
    {
        quitWindow.SetActive(true);
        StartCoroutine(AnimateQuitWindow(true));
    }

    public void OnCancelButton(bool isStartWindow)
    {
        if (isStartWindow)
        {
            StartCoroutine(AnimateStartWindow(false));
        }
        else
        {
            StartCoroutine(AnimateQuitWindow(false));
        }
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

    private IEnumerator AnimateStartWindow(bool isOpening)
    {
        float targetOverlayAlpha = isOpening ? 0.85f : 0f;
        float overlayAlpha = isOpening ? 0f : 0.85f;

        float polaroidTargetX = isOpening ? -185f : -600f;
        float optionsTargetX = isOpening ? 240f : 600f;

        float overlayAnimationTime = 0f;
        while (overlayAnimationTime < 1f)
        {
            overlayAnimationTime += Time.deltaTime * startOverlaySpeed;
            Color overlayColor = startOverlayBackground.color;
            overlayColor.a = Mathf.Lerp(overlayAlpha, targetOverlayAlpha, overlayAnimationTime);
            startOverlayBackground.color = overlayColor;
            yield return null;
        }

        float polaroidAnimationTime = 0f;
        Vector2 initialPolaroidPosition = startPolaroid.anchoredPosition;
        Vector2 targetPolaroidPosition = new Vector2(polaroidTargetX, startPolaroid.anchoredPosition.y);
        while (polaroidAnimationTime < 1f)
        {
            polaroidAnimationTime += Time.deltaTime * startPolaroidSpeed;
            startPolaroid.anchoredPosition = Vector2.Lerp(initialPolaroidPosition, targetPolaroidPosition, polaroidAnimationTime);
            yield return null;
        }

        float optionsAnimationTime = 0f;
        Vector2 initialOptionsPosition = startOptionsWindow.anchoredPosition;
        Vector2 targetOptionsPosition = new Vector2(optionsTargetX, startOptionsWindow.anchoredPosition.y);
        while (optionsAnimationTime < 1f)
        {
            optionsAnimationTime += Time.deltaTime * startOptionsSpeed;
            startOptionsWindow.anchoredPosition = Vector2.Lerp(initialOptionsPosition, targetOptionsPosition, optionsAnimationTime);
            yield return null;
        }

        if (!isOpening)
        {
            startWindow.SetActive(false);
        }
    }

    private IEnumerator AnimateQuitWindow(bool isOpening)
    {
        float targetOverlayAlpha = isOpening ? 0.85f : 0f;
        float overlayAlpha = isOpening ? 0f : 0.85f;

        float polaroidTargetX = isOpening ? 185f : 600f;
        float optionsTargetX = isOpening ? -225f : -580f;

        float overlayAnimationTime = 0f;
        while (overlayAnimationTime < 1f)
        {
            overlayAnimationTime += Time.deltaTime * quitOverlaySpeed;
            Color overlayColor = quitOverlayBackground.color;
            overlayColor.a = Mathf.Lerp(overlayAlpha, targetOverlayAlpha, overlayAnimationTime);
            quitOverlayBackground.color = overlayColor;
            yield return null;
        }

        float polaroidAnimationTime = 0f;
        Vector2 initialPolaroidPosition = quitPolaroid.anchoredPosition;
        Vector2 targetPolaroidPosition = new Vector2(polaroidTargetX, quitPolaroid.anchoredPosition.y);
        while (polaroidAnimationTime < 1f)
        {
            polaroidAnimationTime += Time.deltaTime * quitPolaroidSpeed;
            quitPolaroid.anchoredPosition = Vector2.Lerp(initialPolaroidPosition, targetPolaroidPosition, polaroidAnimationTime);
            yield return null;
        }

        float optionsAnimationTime = 0f;
        Vector2 initialOptionsPosition = quitOptionsWindow.anchoredPosition;
        Vector2 targetOptionsPosition = new Vector2(optionsTargetX, quitOptionsWindow.anchoredPosition.y);
        while (optionsAnimationTime < 1f)
        {
            optionsAnimationTime += Time.deltaTime * quitOptionsSpeed;
            quitOptionsWindow.anchoredPosition = Vector2.Lerp(initialOptionsPosition, targetOptionsPosition, optionsAnimationTime);
            yield return null;
        }

        if (!isOpening)
        {
            quitWindow.SetActive(false);
        }
    }
}