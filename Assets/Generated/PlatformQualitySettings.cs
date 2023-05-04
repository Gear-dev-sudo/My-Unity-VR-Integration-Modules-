using UnityEngine;
using UnityEngine.UI;

// Require the Transform component for this script to work
[RequireComponent(typeof(Transform))]
public class PlatformQualitySettings : MonoBehaviour
{
    // Enum for different platforms
    public enum PlatformType
    {
        Android,
        SteamVR
    }

    [Tooltip("Select the platform for which you want to adapt the scene.")]
    public PlatformType platformType;

    [Tooltip("Set the quality level for the selected platform.")]
    public int qualityLevel;

    [Tooltip("Set the scale factor for the selected platform.")]
    public float scaleFactor;

    [Tooltip("Set the UI scale factor for the selected platform.")]
    public float uiScaleFactor;

    // Start is called before the first frame update
    void Start()
    {
        // Check the current platform and adapt the scene accordingly
        switch (platformType)
        {
            case PlatformType.Android:
                // Set the scale factor for the selected platform
                transform.localScale *= scaleFactor;

                // Set the UI scale factor for the selected platform
                CanvasScaler[] canvasScalers = FindObjectsOfType<CanvasScaler>();
                foreach (CanvasScaler canvasScaler in canvasScalers)
                {
                    canvasScaler.scaleFactor *= uiScaleFactor;
                }

                // Set the quality level for the selected platform
                QualitySettings.SetQualityLevel(qualityLevel, true);

                // Set particle system settings for the selected platform
                ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
                foreach (ParticleSystem particleSystem in particleSystems)
                {
                    particleSystem.maxParticles /= 2;
                }
                break;

            case PlatformType.SteamVR:
                // Set the scale factor for the selected platform
                transform.localScale *= scaleFactor;

                // Set the UI scale factor for the selected platform
                canvasScalers = FindObjectsOfType<CanvasScaler>();
                foreach (CanvasScaler canvasScaler in canvasScalers)
                {
                    canvasScaler.scaleFactor *= uiScaleFactor * 2;
                }

                // Set the quality level for the selected platform
                QualitySettings.SetQualityLevel(qualityLevel, true);

                // Set particle system settings for the selected platform
                particleSystems = FindObjectsOfType<ParticleSystem>();
                foreach (ParticleSystem particleSystem in particleSystems)
                {
                    particleSystem.maxParticles *= 2;
                }
                break;

            default:
                break;
        }
    }
}