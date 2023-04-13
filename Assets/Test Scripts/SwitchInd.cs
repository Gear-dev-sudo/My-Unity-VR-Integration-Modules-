using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInd : MonoBehaviour
{
    TMPro.TMP_Text text;
    private bool isFlashing = false;
    AudioSource audioSource;
    public bool switchPullRequired = false;
    bool alarmTriggable;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        text.alpha = 0;
        audioSource = GetComponent<AudioSource>();
        alarmTriggable = true;
    }
    public void switchEnable()
    {
        switchPullRequired = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (switchPullRequired)
        {

            text.alpha = 255;
            Alarm();
        } 
        alarmTriggable = false;
        if (!switchPullRequired)
        {
            alarmTriggable = true;
            text.alpha = 0;
        }
       
    }

    private void Alarm()
    {
        if (!audioSource.isPlaying && alarmTriggable)
        { 
            audioSource.Play();
            Debug.LogWarning("Playing");
        }
        StartCoroutine(FlashText());
        

    }
    private IEnumerator FlashText()
    {
        while (switchPullRequired)
        {
            if (isFlashing)
            {
                text.color = Color.black;
                isFlashing = false;
            }
            else
            {
                text.color = Color.green;
                isFlashing = true;
            }
            yield return new WaitForSeconds(0.9f);
        }
    }
}
