using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [Tooltip("The audio clip to play.")]
    public AudioClip audioClip;

    [Tooltip("The volume of the audio.")]
    public float volume = 1f;

    private AudioSource audioSource;
    public GameObject fireIndGameObject;
    public MyScoreSingleton myScoreSingleton;

    private void Start()
    {
        myScoreSingleton = FindObjectOfType<MyScoreSingleton>();
        

        fireIndGameObject = FindObjectOfType<FireIndScript>().gameObject;
        fireIndGameObject.GetComponent<FireIndScript>().FireStarted = true;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        
        InvokeRepeating("minusPoints",1,2);
    }

  
    private void Update()
    {


       
      
    }
    void OnDestroy()
    {
        {
            fireIndGameObject.GetComponent<FireIndScript>().FireStarted = false;
            fireIndGameObject.GetComponent<TMPro.TMP_Text>().alpha = 0;
        }
    }
    void minusPoints()
    {
        myScoreSingleton.score -= 1;
    }
}
