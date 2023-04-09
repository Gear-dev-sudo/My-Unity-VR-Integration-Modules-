using UnityEngine;

public class RandomizedSpotlightColor : MonoBehaviour
{
    [Tooltip("The minimum value for the random color range.")]
    public float minColorValue = 0.0f;

    [Tooltip("The maximum value for the random color range.")]
    public float maxColorValue = 1.0f;

    [Tooltip("The first spotlight to randomize the color.")]
    
    public Light spotlight1;

    [Tooltip("The second spotlight to randomize the color.")]
    
    public Light spotlight2;

    private Color randomColor;

    private void Start()
    {
        // Generate a random color within the specified range.
        randomColor = new Color(Random.Range(minColorValue, maxColorValue), Random.Range(minColorValue, maxColorValue), Random.Range(minColorValue, maxColorValue));

        // Set the color of both spotlights to the same random color.
        spotlight1.color = randomColor;
        spotlight2.color = randomColor;
    }
}
