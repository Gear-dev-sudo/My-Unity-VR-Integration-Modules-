using System.Diagnostics;
using UnityEngine;

public class PythonRunner1 : MonoBehaviour
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
        process.StartInfo.CreateNoWindow = !runInBackground;

        // Start the process.
        process.Start();

        // Print the output of the process to the console.
        if (printOutput)
        {
            string output = process.StandardOutput.ReadToEnd();
            UnityEngine.Debug.Log(output);
        }

        // Wait for the process to exit.
        process.WaitForExit();
    }
}