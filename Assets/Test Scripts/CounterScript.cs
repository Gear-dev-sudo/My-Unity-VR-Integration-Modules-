using System.Collections;
using UnityEngine;

public class CounterScript : MonoBehaviour
{

    public OnTiltAddCount wateringCan;

    [Tooltip("The maximum value for the counter.")]
    public int maxCounterValue = 100;

    [Tooltip("The current value of the counter.")]
    public float currentCounterValue = 0;

    [Tooltip("The time interval in seconds to update the counter.")]
    public float updateInterval = 1f;

    private float timer = 0f;

    [Tooltip("The alarm sound to play when the counter is above 75.")]
    public AudioClip alarmSound;
    private TMPro.TMP_Text humidCnt;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        humidCnt = GetComponent<TMPro.TMP_Text>();
    }

    private void Update()
    {

        humidCnt.text = Mathf.Round(currentCounterValue).ToString();
        timer += Time.deltaTime * 10;

        if (timer >= updateInterval && !wateringCan.pouringWater)
        {
            if (currentCounterValue > 20)
                currentCounterValue-=0.1f;
        }


        if (timer >= updateInterval&&wateringCan.pouringWater)
        {
            timer = 0f;
            currentCounterValue++;

            if (currentCounterValue > maxCounterValue)
            {
                currentCounterValue = maxCounterValue;
            }

            if (currentCounterValue > 75)
            {
                Alarm();
                FindObjectOfType<SwitchInd>().switchPullRequired = true;
            }
        }
    }
   // bool alarmPlaying=false;
    private void Alarm()
    {
       // if (alarmPlaying = false)
        {
            //   alarmPlaying = true;
            if(!audioSource.isPlaying)
            audioSource.Play();
           
         StartCoroutine(FlashText());
         }

    }
    IEnumerator FlashText()
    {
        humidCnt.color = Color.blue;

        yield return new WaitForSeconds(0.5f);

        humidCnt.color = Color.red;
        //alarmPlaying = false;

    }
}
