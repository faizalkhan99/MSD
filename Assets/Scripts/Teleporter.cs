using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Transform _teleportDestination;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float teleportDelay = 2f;
    [SerializeField] private string teleportText = "Teleporting...";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsTeleporting())
        {
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    private bool IsTeleporting()
    {
        return fadePanel.color.a > 0.99f; // Check if the screen is currently fading
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        // Fade Out
        yield return StartCoroutine(FadeOut());

        // Teleport after the fade out
        player.transform.position = _teleportDestination.position;

        // Display teleporting text
        yield return StartCoroutine(DisplayText(teleportText));

        // Fade In
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = Color.black;
    }

    private IEnumerator DisplayText(string startText)
    {
        displayText.text = startText;

        // Fade In Text
        yield return StartCoroutine(FadeInText());

        // Wait for teleport delay
        yield return new WaitForSeconds(teleportDelay);

        // Fade Out Text
        yield return StartCoroutine(FadeOutText());

        displayText.text = "";
    }

    private IEnumerator FadeInText()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            displayText.color = new Color(1f, 1f, 1f, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        displayText.color = Color.white;
    }

    private IEnumerator FadeOutText()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            displayText.color = new Color(1f, 1f, 1f, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        displayText.color = Color.clear;
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = Color.clear;
    }



    /*
    [SerializeField] private float _scaleMultiplier;
    private IEnumerator Wobble()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * _scaleMultiplier;
        yield return new WaitForSeconds(0.2f);
        transform.localScale = originalScale;
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(Wobble());
    }
    */
}
