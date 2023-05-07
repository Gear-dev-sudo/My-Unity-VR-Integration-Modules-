using System.Collections;
using UnityEngine;

public class BlinkingLights : MonoBehaviour
{
    [Tooltip("The minimum blinking rate in seconds.")]
    public float minBlinkRate = 1f;
    
    [Tooltip("The maximum blinking rate in seconds.")]
    public float maxBlinkRate = 5f;

        
    [Tooltip("The colors to choose from for the blinking light.")]
    [SerializeField]
    public Color[] colors = { Color.blue, Color.green };

    private Light[] lights;

    private void Start()
    {
        // Get all child Point Light components
        lights = GetComponentsInChildren<Light>();

        // Start the blinking coroutine for each light
        foreach (Light light in lights)
        {
            StartCoroutine(Blink(light));
        }
    }

    private IEnumerator Blink(Light light)
    {
        while (true)
        {
            // Choose a random color from the colors array
            Color randomColor = colors[Random.Range(0, colors.Length)];

            // Set the light color to the random color
            light.color = randomColor;

            // Turn the light on
            light.enabled = true;

            // Wait for a random amount of time between minBlinkRate and maxBlinkRate
            yield return new WaitForSeconds(Random.Range(minBlinkRate, maxBlinkRate));

            // Turn the light off
            light.enabled = false;

            // Wait for a random amount of time between minBlinkRate and maxBlinkRate
            yield return new WaitForSeconds(Random.Range(minBlinkRate, maxBlinkRate));
        }
    }
}
