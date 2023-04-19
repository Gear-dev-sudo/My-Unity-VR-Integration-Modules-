using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIndScript : MonoBehaviour
{
    TMPro.TMP_Text text;
    AudioSource audioSource;
    public bool FireStarted=false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        text.alpha = 0;
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (FireStarted)
        {
            text.alpha = 255;
            Alarm();
            FindObjectOfType<SwitchInd>().switchPullRequired = true;
        }
        else
        {
            text.alpha = 0;
        }
    }

    private void Alarm()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        StartCoroutine(FlashText());


    }
    IEnumerator FlashText()
    {
        text.color = Color.black;

        yield return new WaitForSeconds(0.5f);

        text.color = Color.red;
        //alarmPlaying = false;

    }
}
