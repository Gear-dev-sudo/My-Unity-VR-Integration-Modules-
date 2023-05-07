using System;
using System.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;


public class ControllerRecorder : MonoBehaviour
{
    [Tooltip("Choose which buttons to check")]
    public InputHelpers.Button[] buttons = new InputHelpers.Button[]
    {
        InputHelpers.Button.Trigger,
        InputHelpers.Button.Grip,
        InputHelpers.Button.PrimaryButton,
        InputHelpers.Button.SecondaryButton
    };



    private List<Dictionary<InputHelpers.Button, bool>> buttonStates;

    public XRController controller;
    public string filePath = "log_file.txt";

    void Start()
    {
        buttonStates = new List<Dictionary<InputHelpers.Button, bool>>();

        controller = GetComponent<XRController>();
    }

    void Update()
    {
        Dictionary<InputHelpers.Button, bool> currentButtonState = new Dictionary<InputHelpers.Button, bool>();
      

        foreach (InputHelpers.Button button in buttons)
        {
            bool pressed;
            controller.inputDevice.IsPressed(button, out pressed);
            currentButtonState[button] = pressed;
        }


        buttonStates.Add(currentButtonState);

    }

    void OnDisable()
    {
        Debug.Log("Recording finished");

        for (int i = 0; i < buttonStates.Count; i++)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            File.AppendAllText(filePath, "Time: " + timeStamp + "\n");

            foreach (KeyValuePair<InputHelpers.Button, bool> pair in buttonStates[i])
            {
                File.AppendAllText(filePath, pair.Key + ": " + pair.Value + "\n");
            }

            File.AppendAllText(filePath, "\n");
        }
    }
}