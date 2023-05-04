using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PythonRunner : MonoBehaviour
{
    [Tooltip("The path to the Python executable.")]
    public string pythonPath = "python";

    [Tooltip("The path to the Python script to run.")]
    public string scriptPath;

    [Tooltip("The arguments to pass to the Python script.")]
    public string arguments;

    [Tooltip("Whether to run the Python script in the background.")]
    public bool runInBackground = true;

    [Tooltip("Whether to print the output of the Python script to the console.")]
    public bool printOutput = true;

    // Runs the Python script.
    public void RunScript()
    {
        // Create a new process to run the Python script.
        Process process = new Process();

        // Set the process start info.
        process.StartInfo.FileName = pythonPath;
        process.StartInfo.Arguments = scriptPath + " " + arguments;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = !runInBackground;

        // Start the process.
        process.Start();

        // Read the output of the process.
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        // Wait for the process to exit.
        process.WaitForExit();

        // Print the output to the console if necessary.
        if (printOutput)
        {
            Debug.Log(output);
            Debug.LogError(error);
        }
    }


    public void RunScriptInEditor()
    {
        // Check if the script path is valid.
        if (string.IsNullOrEmpty(scriptPath))
        {
            Debug.LogError("Script path is not set.");
            return;
        }

        // Check if the Python executable exists.
        if (!System.IO.File.Exists(pythonPath))
        {
            Debug.LogError("Python executable not found.");
            return;
        }

        // Run the script.
        RunScript();
    }
}