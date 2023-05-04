using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class SettingScript : MonoBehaviour
{
    [Tooltip("Render pipeline assets for different quality levels on different platforms.")]
    public PlatformQuality[] platformQualities;

    private TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();

        // Add quality level options to dropdown
        List<string> options = new List<string>();
        foreach (PlatformQuality platformQuality in platformQualities)
        {
            options.Add(platformQuality.platform.ToString());
        }
        dropdown.AddOptions(options);
      

        // Set initial quality level based on current platform
        PlatformQuality currentPlatformQuality = GetCurrentPlatformQuality();
        QualitySettings.SetQualityLevel(currentPlatformQuality.qualityLevel);
        QualitySettings.renderPipeline = currentPlatformQuality.renderPipeline;
        dropdown.value = currentPlatformQuality.platformIndex;

        // Add listener to dropdown to change quality level
        dropdown.onValueChanged.AddListener(delegate { ChangeLevel(dropdown.value); });
    }

    public void ChangeLevel(int value)
    {
        PlatformQuality platformQuality = platformQualities[value];
        QualitySettings.SetQualityLevel(platformQuality.qualityLevel);
        QualitySettings.renderPipeline = platformQuality.renderPipeline;
    }

    private PlatformQuality GetCurrentPlatformQuality()
    {
        // Get current platform
        Platform currentPlatform;
        #if UNITY_ANDROID
            currentPlatform = Platform.Android;
        #elif UNITY_IOS
            currentPlatform = Platform.iOS;
        #else
            currentPlatform = Platform.PC;
        #endif

        // Find platform quality for current platform
        foreach (PlatformQuality platformQuality in platformQualities)
        {
            if (platformQuality.platform == currentPlatform)
            {
                return platformQuality;
            }
        }

        // If no platform quality found, return default
        return platformQualities[0];
    }
}

[System.Serializable]
public class PlatformQuality
{
    [Tooltip("Platform for this quality level.")]
    public Platform platform;
    [Tooltip("Quality level for this platform.")]
    public int qualityLevel;
    [Tooltip("Render pipeline asset for this quality level.")]
    public RenderPipelineAsset renderPipeline;
    [Tooltip("Index of the platform in the dropdown list.")]
    public int platformIndex;
}

public enum Platform
{
    PC,
    Android,
    iOS
}