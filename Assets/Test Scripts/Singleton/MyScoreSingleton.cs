using UnityEngine;

/*
 * Keeps track of a highscore and persists it between scenes and sessions.
 */

public class MyScoreSingleton : MonoBehaviour
{
    [Tooltip("The current highscore.")]
    public int highscore = 0;
    public string highscoreName;
    public string currentName;

    [Tooltip("The current score.")]
    public int score = 0;

    private BudgetController budgetController;

    private void Awake()
    {
        // Load the highscore from player prefs.
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreName = PlayerPrefs.GetString("HighscoreName", "");
        budgetController = GameObject.FindObjectOfType<BudgetController>();
    }

    private void Start()
    {
        // Set the highscore if it exists.
        if (highscore > 0)
        {
            SetHighScore(highscore);
        }
    }

    public void ComputeScore(int delta)
    {
        // Update the score.
        score += delta;
        if(budgetController.Budget<0)
        {
            score += budgetController.Budget;
        }

        // Check if the score is higher than the highscore.
        if (score > highscore)
        {
            SetHighScore(score);
        }
    }

    private void SetHighScore(int value)
    {
        // Update the highscore.
        highscore = value;

        // Save the highscore to player prefs.
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.SetString("HighscoreName", currentName);

        // Notify listeners of the highscore change.
        SendMessage("OnHighscoreChanged", highscore, SendMessageOptions.DontRequireReceiver);
    }
}
