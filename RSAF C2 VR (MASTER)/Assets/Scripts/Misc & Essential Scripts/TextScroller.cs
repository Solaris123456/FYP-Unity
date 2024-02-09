using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextScroller : MonoBehaviour
{
    public Text textElement;
    public string textContent;
    public float scrollSpeed = 20f;

    private bool isScrolling;
    private Coroutine scrollCoroutine;

    private void Start()
    {
        // Ensure the text element is initially disabled
        textElement.enabled = false;
    }

    public void StartScrolling()
    {
        if (isScrolling)
            return;

        // Set the text content and enable the text element
        textElement.text = textContent;
        textElement.enabled = true;

        // Start the scrolling coroutine
        scrollCoroutine = StartCoroutine(ScrollText());
        isScrolling = true;
    }

    public void StopScrolling()
    {
        if (!isScrolling)
            return;

        // Disable the text element and stop the scrolling coroutine
        textElement.enabled = false;
        StopCoroutine(scrollCoroutine);
        isScrolling = false;
    }

    private IEnumerator ScrollText()
    {
        // Calculate the initial position of the text element
        Vector2 startPosition = textElement.rectTransform.anchoredPosition;
        Vector2 currentPosition = startPosition;

        // Scroll the text vertically
        while (true)
        {
            currentPosition.y += scrollSpeed * Time.deltaTime;
            textElement.rectTransform.anchoredPosition = currentPosition;

            // Check if the text has scrolled beyond its height, then reset its position
            if (currentPosition.y >= startPosition.y + textElement.rectTransform.rect.height)
                currentPosition.y = startPosition.y;

            yield return null;
        }
    }
}