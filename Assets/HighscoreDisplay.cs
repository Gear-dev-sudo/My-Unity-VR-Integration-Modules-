using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField]
    MyScoreSingleton myScoreSingleton;

    [SerializeField]
    TMPro.TMP_Text scoreText;
    
    


    // Start is called before the first frame update
    void Awake()
    {
        myScoreSingleton = GameObject.FindObjectOfType<MyScoreSingleton>();
        scoreText = GetComponent<TMPro.TMP_Text>();
        scoreText.text = ("High Score: "+myScoreSingleton.highscore+" Record Holder:"+myScoreSingleton.highscoreName+"\n"+ "Score: " + myScoreSingleton.score );
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ("High Score: " + myScoreSingleton.highscore + " Record Holder:" + myScoreSingleton.highscoreName+ "\n" + "Score: " + myScoreSingleton.score);
    }
}
