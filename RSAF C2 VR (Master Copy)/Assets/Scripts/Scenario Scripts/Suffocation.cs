using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Suffocation : MonoBehaviour
{
    public GameObject AirTag;
    public float activationTime = 10f; // Time in seconds before activation event triggers
    public UnityEvent activationEvent;
    public Renderer fadeRenderer;
    public float fadeDuration = 1f;

    private bool isActivated = false;
    private float elapsedTime = 0f;
    private bool isFading = false;
    private Color startColor;

    private void Start()
    {
        // Get the initial color of the fade renderer
        startColor = fadeRenderer.material.color;
    }

    private void Update()
    {
        // Check if the target object is active
        if (AirTag.activeSelf)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Check if the elapsed time has exceeded the activation time
            if (!isActivated && elapsedTime >= activationTime)
            {
                // Trigger the activation event
                activationEvent.Invoke();
                isActivated = true;
            }

            // Fade the screen to black gradually as the elapsed time increases
            if (!isFading && elapsedTime >= activationTime - fadeDuration)
            {
                StartCoroutine(FadeScreenToBlack());
            }
        }
        else
        {
            // Reset the timer if the target object becomes inactive
            elapsedTime = 0f;
            isActivated = false;

            // Reset the screen back to normal
            fadeRenderer.material.color = startColor;
        }
    }

    private IEnumerator FadeScreenToBlack()
    {
        isFading = true;

        float startTime = Time.time;
        float startAlpha = fadeRenderer.material.color.a;
        float targetAlpha = 0f;

        while (fadeRenderer.material.color.a > targetAlpha)
        {
            float elapsedTime = Time.time - startTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeDuration);

            Color color = fadeRenderer.material.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            fadeRenderer.material.color = color;

            yield return null;
        }

        isFading = false;
    }
}