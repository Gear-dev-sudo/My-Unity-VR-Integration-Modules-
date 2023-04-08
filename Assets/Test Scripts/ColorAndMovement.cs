using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorAndMovement : MonoBehaviour
{
    [Tooltip("Speed of color change")]
    public float colorChangeSpeed = 1f;
    [Tooltip("Frequency of oscillation")]
    public float oscillationFrequency = 1f;
    [Tooltip("Amplitude of oscillation")]
    public float oscillationAmplitude = 1f;

    private MeshRenderer meshRenderer;
    private float initialYPosition;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialYPosition = transform.position.y;
    }

    private void Update()
    {
        // Change hue of material
        float hue = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
        meshRenderer.material.color = Color.HSVToRGB(hue, 1f, 1f);

        // Oscillate vertically
        float yPosition = initialYPosition + Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude * transform.localScale.y;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }
}
