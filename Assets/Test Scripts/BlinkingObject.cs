using System.Collections;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    [Tooltip("The time in seconds between each blink.")]
    public float blinkInterval = 1f;

    [Tooltip("The duration of each blink in seconds.")]
    public float blinkDuration = 0.5f;

    private Renderer objectRenderer;
    private Color originalColor;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        InvokeRepeating("Blink", blinkInterval, blinkInterval);
    }

    private void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        float elapsedTime = 0f;

        Color randomColor = Color.green ;

        while (elapsedTime < blinkDuration)
        {
            objectRenderer.material.color = randomColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectRenderer.material.color = originalColor;
    }
}
