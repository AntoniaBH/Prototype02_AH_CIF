using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject mainMenuWindow;
    public GameObject quitWindow;
    public Image overlayBackground;
    public RectTransform polaroid;
    public RectTransform mainMenuBackground;
    public RectTransform quitBackground;
    public Image mainMenuTransition;
    public Image quitTransition;

    private bool isPaused = false;
    private Coroutine animationCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(OpenPauseMenu());
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(ClosePauseMenu());
    }

    private IEnumerator OpenPauseMenu()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color bgColor = overlayBackground.color;
        Vector2 startPolaroidPos = new Vector2(0, 450);
        Vector2 endPolaroidPos = new Vector2(0, 0);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            overlayBackground.color = new Color(bgColor.r, bgColor.g, bgColor.b, Mathf.Lerp(0, 0.95f, t));
            polaroid.anchoredPosition = Vector2.Lerp(startPolaroidPos, endPolaroidPos, t);
            yield return null;
        }
    }

    private IEnumerator ClosePauseMenu()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color bgColor = overlayBackground.color;
        Vector2 startPolaroidPos = new Vector2(12, 0);
        Vector2 endPolaroidPos = new Vector2(12, -450);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            overlayBackground.color = new Color(bgColor.r, bgColor.g, bgColor.b, Mathf.Lerp(0.95f, 0, t));
            polaroid.anchoredPosition = Vector2.Lerp(startPolaroidPos, endPolaroidPos, t);
            yield return null;
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenMainMenuWindow()
    {
        mainMenuWindow.SetActive(true);
        StartCoroutine(SlideIn(mainMenuBackground));
    }

    public void OpenQuitWindow()
    {
        quitWindow.SetActive(true);
        StartCoroutine(SlideIn(quitBackground));
    }

    public void CloseMainMenuWindow()
    {
        StartCoroutine(SlideOut(mainMenuBackground, mainMenuWindow));
    }

    public void CloseQuitWindow()
    {
        StartCoroutine(SlideOut(quitBackground, quitWindow));
    }

    public void ConfirmMainMenu()
    {
        mainMenuTransition.gameObject.SetActive(true);
        StartCoroutine(FadeAndLoadScene(mainMenuTransition, "MainMenu"));
    }

    public void ConfirmQuit()
    {
        quitTransition.gameObject.SetActive(true);
        StartCoroutine(FadeAndQuit(quitTransition));
    }

    private IEnumerator SlideIn(RectTransform window)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector2 startPos = new Vector2(12, -450);
        Vector2 endPos = new Vector2(12, 0);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            window.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

    private IEnumerator SlideOut(RectTransform window, GameObject windowObject)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector2 startPos = new Vector2(12, 0);
        Vector2 endPos = new Vector2(12, -450);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            window.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        windowObject.SetActive(false);
    }

    private IEnumerator FadeAndLoadScene(Image transitionImage, string sceneName)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color color = transitionImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            transitionImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator FadeAndQuit(Image transitionImage)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color color = transitionImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            transitionImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        Application.Quit();
    }
}


