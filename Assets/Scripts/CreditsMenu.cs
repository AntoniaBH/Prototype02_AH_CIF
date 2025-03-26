using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public float scrollSpeed = 35f;
    public RectTransform creditsText;
    public TextMeshProUGUI titleText;

    public float fadeDuration = 1.5f;
    public float startPositionY = -650;
    public float endPositionY;

    public float timeBeforeFading = 2f;

    private void Start()
    {
        startPositionY = -650;
        endPositionY = Screen.height;

        creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, startPositionY);

        StartCoroutine(FadeTitle());

        StartCoroutine(ScrollCredits());
    }

    private IEnumerator ScrollCredits()
    {
        while (creditsText.anchoredPosition.y < endPositionY)
        {
            creditsText.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator FadeTitle()
    {
        yield return new WaitForSeconds(timeBeforeFading);

        Color titleColor = titleText.color;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            titleColor.a = alpha;

            titleText.color = titleColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        titleColor.a = 0f;

        titleText.color = titleColor;
    }
}
